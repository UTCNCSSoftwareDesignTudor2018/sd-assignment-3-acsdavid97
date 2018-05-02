using System;
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

            var server = container.Resolve<Server>();
            server.Start();
        }

    }
}