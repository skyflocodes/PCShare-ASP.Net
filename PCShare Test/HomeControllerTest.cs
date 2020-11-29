using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCShare.Controllers;

namespace PCShare_Test
{
    [TestClass]
    public class HomeControllerTest
    {
         
        [TestMethod]
        public void IndexLoggerResult()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            ILogger<HomeController> logger = new Logger<HomeController>(loggerFactory);
            HomeController controller = new HomeController(logger);

            //act
            var result = controller.Index();

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PrivacyResult()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            ILogger<HomeController> logger = new Logger<HomeController>(loggerFactory);
            HomeController controller = new HomeController(logger);

            //act
            var result = controller.Privacy();

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void privacyLoadsPrivacyView()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            ILogger<HomeController> logger = new Logger<HomeController>(loggerFactory);
            HomeController controller = new HomeController(logger);

            //act
            var result = (ViewResult)controller.Privacy();

            //assert
            Assert.AreEqual("Privacy", result.ViewName);
        }
    }
}
