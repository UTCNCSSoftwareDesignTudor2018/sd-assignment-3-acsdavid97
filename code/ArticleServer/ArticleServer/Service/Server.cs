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
using ArticleServer.Service.Command;
using AutoMapper;

namespace ArticleServer.Service
{
    public class Server 
    {
        private readonly IMapper _mapper;
        private readonly ArticleBll _articleBll;
        private readonly WriterBll _writerBll;
        private readonly IList<NetworkStream> _connectedClients;
        private readonly ManualResetEvent _acceptDone = new ManualResetEvent(false);

        public Server(ArticleBll articleBll, IMapper mapper, WriterBll writerBll1)
        {
            _articleBll = articleBll;
            _writerBll = writerBll1;
            _mapper = mapper;
            _connectedClients = new List<NetworkStream>();
        }

        public void Start()
        {
            var localIp = IPAddress.Parse("127.0.0.1");
            var server = new TcpListener(localIp, 11000);

            var updateThread = new Thread(UpdateThread);
            updateThread.Start();

            server.Start();

            while (true)
            {
                _acceptDone.Reset();
                Console.WriteLine("waiting for connection");
                server.BeginAcceptTcpClient(HandleClient, server);
                _acceptDone.WaitOne();
            }
        }

        private void UpdateThread()
        {
            var localIp = IPAddress.Parse("127.0.0.1");
            var server = new TcpListener(localIp, 11001);

            server.Start();

            while (true)
            {
                Console.WriteLine("waiting for update client");
                var client = server.AcceptTcpClient();
                Console.WriteLine("update client accepted");
                var stream = client.GetStream();
                lock (_connectedClients)
                {
                    _connectedClients.Add(stream);
                }
                Console.WriteLine("update client added");
            }
        }

        private void HandleClient(IAsyncResult ar)
        {
            var server = (TcpListener) ar.AsyncState;
            var client = server.EndAcceptTcpClient(ar);
            _acceptDone.Set();
            var stream = client.GetStream();

            Console.WriteLine("Client connected");
            while (true)
            {
                var message = Utils.ReadObject<string>(stream);
                if (message == Constants.GetArticlesCommand)
                {
                    HandleGetArticles(stream);
                }

                if (message == Constants.UpdateArticleCommand)
                {
                    HandleUpdateArticle(stream);
                }

                if (message == Constants.AddArticleCommand)
                {
                    HandleAddArticle(stream);
                }

                if (message == Constants.ExitCommand)
                {
                    HandleExit(stream);
                    break;
                }
            }

            client.Close();
        }

        private void HandleUpdateArticle(NetworkStream stream)
        {
            try
            {
                Console.WriteLine("Client updating article");
                Utils.SendObject(Constants.Success, stream);
                var articleUpdateDto = Utils.ReadObject<ArticleUpdateDto>(stream);
                var articleDto = articleUpdateDto.ArticleDto;
                var writerDto = articleUpdateDto.WriterDto;
                var article = _mapper.Map<Article>(articleDto);
                article.Writer = _writerBll.FindWriter(writerDto.Name, writerDto.Password);
                _articleBll.UpdateArticle(article);
                Utils.SendObject(Constants.Success, stream);

                Console.WriteLine("Successfully updated article");

                NotifyObservers();
            }
            catch (Exception e)
            {
                Utils.SendObject(Constants.Error, stream);
            }
        }

        private void HandleExit(NetworkStream stream)
        {
            Console.WriteLine("Client exiting");
            lock (_connectedClients)
            {
                _connectedClients.Remove(stream);
                stream.Close();
                return;
            }
        }

        private void HandleAddArticle(NetworkStream stream)
        {
            try
            {
                Console.WriteLine("Client adding article");
                Utils.SendObject(Constants.Success, stream);
                var articleUpdateDto = Utils.ReadObject<ArticleUpdateDto>(stream);
                var articleDto = articleUpdateDto.ArticleDto;
                var writerDto = articleUpdateDto.WriterDto;
                var article = _mapper.Map<Article>(articleDto);
                article.Writer = _writerBll.FindWriter(writerDto.Name, writerDto.Password);
                _articleBll.AddArticle(article);
                Utils.SendObject(Constants.Success, stream);

                Console.WriteLine("Successfully added article");

                NotifyObservers();
            }
            catch (Exception e)
            {
                Utils.SendObject(Constants.Error, stream);
            }
        }

        private void HandleGetArticles(NetworkStream stream)
        {
            Console.WriteLine("Client requesting articles");
            var articles = _articleBll.GetArticles();
            var articleDtos = _mapper.Map<ArticleDto[]>(articles);
            Utils.SendObject(articleDtos, stream);
        }

        private void NotifyObservers()
        {
            lock (_connectedClients)
            {
                foreach (var connected in _connectedClients)
                {
                    Utils.SendObject(Constants.Update, connected);
                }
            }
        }
    }
}
