using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ArticleServer.Business.Entity;
using ArticleServer.Business.Validator;
using ArticleServer.DataAccess.Repository;

namespace ArticleServer.Business.Process
{
    public class ArticleBll
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleValidator _validator;

        public ArticleBll(IArticleRepository articleRepository, IArticleValidator validator)
        {
            _articleRepository = articleRepository;
            _validator = validator;
        }

        public IList<Article> GetArticles()
        {
            IList<Article> articles = null;
            lock (_articleRepository)
            {
                articles = _articleRepository.GetAll();
            }
            return articles;
        }

        public void AddArticle(Article article)
        {
            _validator.Validate(article);
            lock (_articleRepository)
            {
                _articleRepository.Insert(article);
            }
        }

        public void UpdateArticle(Article article)
        {
            _validator.Validate(article);
            lock (_articleRepository)
            {
                _articleRepository.Update(article);
            }
        }

        public void DeleteArticle(Article article)
        {
            lock (_articleRepository)
            {
                _articleRepository.Delete(article);
            }
        }
    }
}
