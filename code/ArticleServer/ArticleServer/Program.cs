using System;
using System.Data.Entity;
using ArticleServer.Business.Validator;
using ArticleServer.DataAccess.Database;
using ArticleServer.DataAccess.Repository;
using ArticleServer.Service;
using ArticleServer.Service.Mapper;
using Unity;

namespace ArticleServer
{
    public class ServerMain
    {
        public static void Main(String[] args)
        {
            var container = new UnityContainer();

            container.RegisterInstance(MapperConfig.Create());
            container.RegisterType<IArticleRepository, ArticleRepository>();
            container.RegisterType<IWriterRepository, WriterRepository>();
            container.RegisterType<IArticleValidator, ArticleValidator>();
            container.RegisterInstance(new ArticleServerDbContext());

            Database.SetInitializer(new ArticleServerDbContextInitializer());

            var server = container.Resolve<Server>();
            server.Start();
        }

    }
}