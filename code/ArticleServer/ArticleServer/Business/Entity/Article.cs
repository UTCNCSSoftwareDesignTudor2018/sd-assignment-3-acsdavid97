using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleServer.Business.Entity
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public virtual WriterUser Writer { get; set; }
        public string Body { get; set; }
    }
}
