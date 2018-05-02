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
        public ArticleServerDbContext() : base("name=ArticleServerDb")
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<WriterUser> Writers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<WriterUser>().ToTable("WriterUser");
        }
    }
}
