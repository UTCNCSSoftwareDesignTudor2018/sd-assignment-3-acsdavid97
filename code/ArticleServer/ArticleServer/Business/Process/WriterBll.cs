using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleServer.Business.Entity;
using ArticleServer.DataAccess.Repository;

namespace ArticleServer.Business.Process
{
    public class WriterBll
    {
        private readonly IWriterRepository _writerRepository;

        public WriterBll(IWriterRepository writerRepository)
        {
            _writerRepository = writerRepository;
        }

        public WriterUser FindWriterById(int writerId)
        {
            return _writerRepository.Get(writerId);
        }

        public WriterUser FindWriter(string name, string password)
        {
            return _writerRepository.Get(name, password);
        }
    }
}
