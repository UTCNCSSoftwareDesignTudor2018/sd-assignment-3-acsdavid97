using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleClientServerCommons.Dto
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public int WriterId { get; set; }
        public string Body { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Title)}: {Title}, {nameof(Abstract)}: {Abstract}, {nameof(WriterId)}: {WriterId}, {nameof(Body)}: {Body}";
        }
    }
}
