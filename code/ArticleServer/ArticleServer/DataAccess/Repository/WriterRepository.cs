using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using ArticleServer.Business.Entity;
using ArticleServer.DataAccess.Database;

namespace ArticleServer.DataAccess.Repository
{
    public class WriterRepository : IWriterRepository
    {
        private readonly ArticleServerDbContext _dbContext;

        public WriterRepository(ArticleServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public WriterUser Get(WriterUser toGet)
        {
            return Get(toGet.Id);
        }

        public WriterUser Get(int id)
        {
            return _dbContext.Writers.FirstOrDefault(w => w.Id == id);
        }

        public IList<WriterUser> GetAll()
        {
            return _dbContext.Writers.ToList();
        }

        public void Insert(WriterUser toInsert)
        {
            _dbContext.Writers.Add(toInsert);
            _dbContext.SaveChanges();
        }

        public void Update(WriterUser toUpdate)
        {
            _dbContext.Writers.AddOrUpdate(toUpdate);
            _dbContext.SaveChanges();
        }

        public void Delete(WriterUser toDelete)
        {
            _dbContext.Writers.Remove(toDelete);
            _dbContext.SaveChanges();
        }

        public WriterUser Get(string name, string password)
        {
            return _dbContext.Writers.FirstOrDefault(w => w.Name == name && w.Password == password);
        }
    }
}