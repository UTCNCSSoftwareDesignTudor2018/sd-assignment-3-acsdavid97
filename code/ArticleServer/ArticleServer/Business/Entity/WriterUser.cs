using System.Collections.Generic;

namespace ArticleServer.Business.Entity
{
    public class WriterUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}