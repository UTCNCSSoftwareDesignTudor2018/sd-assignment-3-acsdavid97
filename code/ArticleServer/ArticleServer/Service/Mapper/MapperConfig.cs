using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleClientServerCommons.Dto;
using ArticleServer.Business.Entity;
using AutoMapper;

namespace ArticleServer.Service.Mapper
{
    class MapperConfig
    {
        private static readonly MapperConfiguration Configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Article, ArticleDto>();
                cfg.CreateMap<ArticleDto, Article>();
                cfg.CreateMap<WriterUser, WriterDto>();
                cfg.CreateMap<WriterDto, WriterUser>();
            });

        public static IMapper Create()
        {
            return Configuration.CreateMapper();
        }
    }
}
