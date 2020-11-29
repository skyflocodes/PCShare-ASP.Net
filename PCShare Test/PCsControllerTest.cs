using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCShare.Controllers;
using PCShare.Data;
using PCShare.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PCShare_Test
{
    [TestClass]
     public class PCsControllerTest
    {
        private ApplicationDbContext _context;
        List<PC> pcs = new List<PC>();
        PCsController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            var user = new User { Id = 999, Username = "randomName", Email = "random@email.com", Password = "123!random"};

            pcs.Add(new PC { Id = 9990, CPU = "FakeCPU 1", GPU = "FakeGPU 1", MOBO = "FakeMOBO 1", User = user });
            pcs.Add(new PC { Id = 990, CPU = "FakeCPU 2", GPU = "FakeGPU 2", MOBO = "FakeMOBO 2", User = user });
            pcs.Add(new PC { Id = 90, CPU = "FakeCPU 3", GPU = "FakeGPU 3", MOBO = "FakeMOBO 3", User = user });

            foreach (var p in pcs)
            {
                _context.Pcs.Add(p);
            }

            _context.SaveChanges();

            controller = new PCsController(_context);
        }

        [TestMethod]
        public void IndexViewLoads()
        {
            var result = controller.Index();
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void IndexReturnsProductData()
        {
            var result = controller.Index();
            var viewResult = (ViewResult)result.Result;
            List<PC> model = (List<PC>)viewResult.Model;

            CollectionAssert.AreEqual(pcs, model);
        }
    }
}
