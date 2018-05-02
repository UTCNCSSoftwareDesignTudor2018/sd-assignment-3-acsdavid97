using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleClientServerCommons.Dto
{
    public class ArticleUpdateDto
    {
        public ArticleDto ArticleDto { get; set; }
        public WriterDto WriterDto { get; set; }
    }
}
