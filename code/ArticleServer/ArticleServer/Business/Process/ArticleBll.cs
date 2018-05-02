using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ArticleServer.Business.Entity;
using ArticleServer.DataAccess.Repository;

namespace ArticleServer.Business.Process
{
    public class ArticleBll
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleBll(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public IList<Article> GetArticles()
        {
            return _articleRepository.GetAll();
        }

        public void AddArticle(Article article)
        {
            _articleRepository.Insert(article);
        }
    }
}
