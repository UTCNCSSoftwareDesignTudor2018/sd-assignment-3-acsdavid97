using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleServer.DataAccess.Repository
{
    public interface IGenericRepository<T>
    {
        T Get(T toGet);
        T Get(int id);
        IList<T> GetAll();
        void Insert(T toInsert);
        void Update(T toUpdate);
        void Delete(T toDelete);
    }
}
