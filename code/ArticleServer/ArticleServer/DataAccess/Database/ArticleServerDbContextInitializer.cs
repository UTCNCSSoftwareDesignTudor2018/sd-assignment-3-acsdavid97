using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleServer.Business.Entity;
using Unity.Interception.Utilities;

namespace ArticleServer.DataAccess.Database
{
    public class ArticleServerDbContextInitializer : DropCreateDatabaseIfModelChanges<ArticleServerDbContext>
    {
        protected override void Seed(ArticleServerDbContext context)
        {
            var writers = new[]
            {
                new WriterUser
                {
                    Articles = new List<Article>(),
                    Name = "first_writer",
                    Password = "first"
                },

                new WriterUser
                {
                    Articles = new List<Article>(),
                    Name = "second_writer",
                    Password = "second"
                },
            };

            writers.ForEach(w => context.Writers.Add(w));
            context.SaveChanges();

            var articles = new[]
            {
                new Article
                {
                    Title = "First title",
                    Abstract = "first abstract",
                    Body = "first body",
                    Writer = writers[0],
                },

                new Article
                {
                    Title = "second",
                    Abstract = "second abs",
                    Body = "second body",
                    Writer = writers[1]
                },

                new Article
                {
                    Title = "Hello world",
                    Abstract = "programming in a nutshell",
                    Body = "import antigravity. Use python",
                    Writer = writers[1]
                }
            };

            articles.ForEach(a => context.Articles.Add(a));
            context.SaveChanges();
        }
    }
}
