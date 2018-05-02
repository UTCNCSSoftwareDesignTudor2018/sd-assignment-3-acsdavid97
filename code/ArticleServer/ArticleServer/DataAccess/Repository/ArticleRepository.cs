using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using ArticleServer.Business.Entity;
using ArticleServer.DataAccess.Database;

namespace ArticleServer.DataAccess.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ArticleServerDbContext _dbContext;

        public ArticleRepository(ArticleServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Article Get(Article toGet)
        {
            return Get(toGet.Id);
        }

        public Article Get(int id)
        {
            return _dbContext.Articles.FirstOrDefault(a => a.Id == id);
        }

        public IList<Article> GetAll()
        {
            return _dbContext.Articles.ToList();
        }

        public void Insert(Article toInsert)
        {
            _dbContext.Articles.Add(toInsert);
            _dbContext.SaveChanges();
        }

        public void Update(Article toUpdate)
        {
            _dbContext.Articles.AddOrUpdate(toUpdate);
            _dbContext.SaveChanges();
        }

        public void Delete(Article toDelete)
        {
            _dbContext.Articles.Remove(toDelete);
            _dbContext.SaveChanges();
        }
    }
}