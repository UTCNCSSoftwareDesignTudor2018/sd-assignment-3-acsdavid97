using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleServer.Business.Entity;

namespace ArticleServer.Business.Validator
{
    public interface IArticleValidator
    {
        void Validate(Article article);
    }

    class ArticleValidator : IArticleValidator
    {
        public void Validate(Article article)
        {
            if (article.Title.Length < 2 || article.Abstract.Length < 2 || article.Body.Length < 2)
            {
                throw new ArgumentException();
            }

            if (article.Writer == null)
            {
                throw new ArgumentException();
            }
        }
    }
}
