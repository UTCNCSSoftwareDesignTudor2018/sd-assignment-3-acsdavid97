using ArticleServer.Business.Entity;

namespace ArticleServer.DataAccess.Repository
{
    public interface IWriterRepository : IGenericRepository<WriterUser>
    {
        WriterUser Get(string name, string password);
    }
}