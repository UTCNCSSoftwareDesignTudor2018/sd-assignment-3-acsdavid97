using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ArticleClientServerCommons;
using ArticleClientServerCommons.Dto;

namespace ArticleGUIClient
{
    public class ArticleClient
    {
        private readonly NetworkStream _stream;

        public ArticleClient()
        {
            var client = new TcpClient("127.0.0.1", 11000);
            _stream = client.GetStream();
        }

        public IList<ArticleDto> GetArticleDtos()
        {
            Utils.SendObject(Constants.GetArticlesCommand, _stream);
            var articles = Utils.ReadObject<ArticleDto[]>(_stream);
            return articles;
        }
    }
}
