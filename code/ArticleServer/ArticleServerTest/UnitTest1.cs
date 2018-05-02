using System;
using System.Data.Entity;
using System.Linq;
using ArticleServer.DataAccess.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArticleServerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var context = new ArticleServerDbContext();
            Database.SetInitializer(new ArticleServerDbContextInitializer());

            Assert.AreEqual(3, context.Articles.Count());
            Assert.AreEqual(2, context.Writers.Count());
        }
    }
}
