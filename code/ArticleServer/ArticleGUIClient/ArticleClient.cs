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

        public bool AddArticle(ArticleDto article, WriterDto writer)
        {
            Utils.SendObject(Constants.AddArticleCommand, _stream);
            var response = Utils.ReadObject<string>(_stream);
            var articleUpdateDto = new ArticleUpdateDto
            {
                ArticleDto = article,
                WriterDto = writer
            };
            Utils.SendObject(articleUpdateDto, _stream);
            var finalResponse = Utils.ReadObject<string>(_stream);
            return finalResponse == Constants.Success;
        }

        public bool UpdateArticle(ArticleDto updated, ArticleDto original, WriterDto writer)
        {
            Utils.SendObject(Constants.UpdateArticleCommand, _stream);
            var response = Utils.ReadObject<string>(_stream);
            updated.Id = original.Id;
            var articleUpdateDto = new ArticleUpdateDto
            {
                ArticleDto = updated,
                WriterDto = writer
            };
            Utils.SendObject(articleUpdateDto, _stream);
            var finalResponse = Utils.ReadObject<string>(_stream);
            return finalResponse == Constants.Success;
        }
    }
}
