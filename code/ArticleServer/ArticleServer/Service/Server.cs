using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArticleClientServerCommons;
using ArticleClientServerCommons.Dto;
using ArticleServer.Business.Entity;
using ArticleServer.Business.Process;
using AutoMapper;

namespace ArticleServer.Service
{
    public class Server
    {
        private readonly IMapper _mapper;
        private readonly ArticleBll _articleBll;
        private readonly IList<NetworkStream> _connectedClients;
        private readonly ManualResetEvent _acceptDone = new ManualResetEvent(false);

        public Server(ArticleBll articleBll, IMapper mapper)
        {
            _articleBll = articleBll;
            _mapper = mapper;
            _connectedClients = new List<NetworkStream>();
        }

        public void Start()
        {
            var localIp = IPAddress.Parse("127.0.0.1");
            var server = new TcpListener(localIp, 11000);

            server.Start();

            while (true)
            {
                _acceptDone.Reset();
                Console.WriteLine("waiting for connection");
                server.BeginAcceptTcpClient(HandleClient, server);
                _acceptDone.WaitOne();
            }
        }

        private void HandleClient(IAsyncResult ar)
        {
            var server = (TcpListener) ar.AsyncState;
            var client = server.EndAcceptTcpClient(ar);
            _acceptDone.Set();
            var stream = client.GetStream();
            lock (_connectedClients)
            {
                _connectedClients.Add(stream);
            }

            Console.WriteLine("Client connected");
            while (true)
            {
                var message = Utils.ReadObject<string>(stream);
                if (message == Constants.GetArticlesCommand)
                {
                    Console.WriteLine("Client requesting articles");
                    var articles = _articleBll.GetArticles();
                    var articleDtos = _mapper.Map<ArticleDto[]>(articles);
                    Utils.SendObject(articleDtos, stream);
                }

                if (message == Constants.AddArticleCommand)
                {
                    Console.WriteLine("Client adding article");
                    var articleDto = Utils.ReadObject<ArticleDto>(stream);
                    var article = _mapper.Map<Article>(articleDto);
                    _articleBll.AddArticle(article);
                }

                if (message == Constants.ExitCommand)
                {
                    Console.WriteLine("Client exiting");
                    lock (_connectedClients)
                    {
                        _connectedClients.Remove(stream);
                        stream.Close();
                        break;
                    }
                }
            }


            client.Close();
        }
    }
}
