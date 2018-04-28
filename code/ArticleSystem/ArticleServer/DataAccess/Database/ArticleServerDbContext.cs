using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleServer.Business.Entity;

namespace ArticleServer.DataAccess.Database
{
    public class ArticleServerDbContext : DbContext
    {
        protected ArticleServerDbContext() : base ("name=ArticleServerDb")
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<WriterUser> Writers { get; set; }
    }
}
