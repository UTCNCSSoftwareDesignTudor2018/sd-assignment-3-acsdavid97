using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleServer.Business.Entity;

namespace ArticleServer.DataAccess.Repository
{
    public interface IArticleRepository : IGenericRepository<Article>
    {
    }
}
