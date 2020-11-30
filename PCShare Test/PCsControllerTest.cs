using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCShare.Controllers;
using PCShare.Data;
using PCShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

            var user = new User { Id = 999, Username = "randomName", Email = "random@email.com", Password = "123!random" };

            pcs.Add(new PC { Id = 9990, CPU = "FakeCPU 1", GPU = "FakeGPU 1", MOBO = "FakeMOBO 1", User = user });
            pcs.Add(new PC { Id = 990, CPU = "FakeCPU 2", GPU = "FakeGPU 2", MOBO = "FakeMOBO 2", User = user });
            pcs.Add(new PC { Id = 90, CPU = "FakeCPU 3", GPU = "FakeGPU 3", MOBO = "FakeMOBO 3", User = user });

            foreach (var p in pcs)
            {
                _context.PC.Add(p);
            }

            _context.SaveChanges();

            controller = new PCsController(_context);
        }

        [TestMethod]
        public void IndexReturnsToIndex()
        {
            var result = controller.Index();
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void IndexReturnsData()
        {
            var result = controller.Index();
            var viewResult = (ViewResult)result.Result;
            List<PC> model = (List<PC>)viewResult.Model;

            CollectionAssert.AreEqual(pcs, model);
        }

        [TestMethod]
        public void CreatePostReturnsToIndex()
        {
            var pc = new PC { Id = 99900, CPU = "FakeCPU 1", GPU = "FakeGPU 1", MOBO = "FakeMOBO 1", User = new User { Id = 9990, Username = "randomName", Email = "random@email.com", Password = "123!random" } };
            var result = controller.Create(pc);
            var redirectResult = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void CreatePostReturnsToCreate()
        {
            var pc = new PC { };
            controller.ModelState.AddModelError("null post", "entered a null pc object");
            var result = controller.Create(pc);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Create", viewResult.ViewName);
        }

        [TestMethod]
        public void CreatePostSavesToDb()
        {
            var pc = new PC { Id = 99900, CPU = "FakeCPU 1", GPU = "FakeGPU 1", MOBO = "FakeMOBO 1", User = new User { Id = 9990, Username = "randomName", Email = "random@email.com", Password = "123!random" } };
            _context.PC.Add(pc);
            _context.SaveChanges();
            
            Assert.AreEqual(pc, _context.PC.ToArray()[3]);
        }

        [TestMethod]
        public void CreatePostNotNull()
        {
            var pc = new PC { };
            controller.ModelState.AddModelError("null post", "entered a null pc object");
            var result = controller.Create(pc);
            var viewResult = (ViewResult)result.Result;

            Assert.IsNotNull(viewResult.ViewData);
        }

        [TestMethod]
        public void DetailsReturnsToDetails()
        {
            int id = pcs[0].Id;
            var result = controller.Details(id);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Details", viewResult.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidId()
        {
            var result = controller.Details(id: -1);
            var notFoundResult = (NotFoundResult)result.Result;

            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [TestMethod]
        public void DetailsNullId()
        {
            var result = controller.Details(id: null);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void DetailReturnsObject()
        {
            int id = pcs[0].Id;
            var result = controller.Details(id);
            var viewResult = (ViewResult)result.Result;
            
            Assert.AreEqual(pcs[0], viewResult.Model);
        }

        [TestMethod]
        public void CreateReturnsToCreate()
        {
            var result = controller.Create();
            var viewResult = (ViewResult)result;

            Assert.AreEqual("Create", viewResult.ViewName);
        }

        [TestMethod]
        public void CreateReturnsList()
        {
            _ = controller.Create();
            var viewData = controller.ViewData["UserId"];

            Assert.IsNotNull(viewData);
        }

        [TestMethod]
        public void GetEditValidId()
        {
            var result = controller.Edit(990);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Edit", viewResult.ViewName);

        }

        [TestMethod]
        public void GetEditInvalidId()
        {
            var result = controller.Edit(-1);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);

        }

        [TestMethod]
        public void GetEditNullId()
        {
            var result = controller.Edit(null);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);

        }

        [TestMethod]
        public void EditSReturnsToIndex()
        {
            var pc = pcs[0];
            pc.CPU = "FakeCPU 9";
            var result = controller.Edit(pc.Id, pc);
            var redirectResult = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void EditReturnsToError()
        {
            var result = controller.Edit(-1, pcs[0]);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void EditLoadsModel()
        {
            var result = controller.Edit(90);
            var viewResult = (ViewResult)result.Result;
            PC model = (PC)viewResult.Model;

            Assert.AreEqual(_context.PC.Find(90), model);
        }

        [TestMethod]
        public void EditLoadsViewData()
        {
            var result = controller.Edit(90);
            var viewResult = (ViewResult)result.Result;
            var viewData = viewResult.ViewData;

            Assert.AreEqual(viewData, viewResult.ViewData);
        }

        [TestMethod]
        public void EditLoadsInvalidId()
        {
            var result = controller.Edit(-1);
            var viewResult = (ViewResult)result.Result;
            PC model = (PC)viewResult.Model;

            Assert.AreNotEqual(_context.PC.FindAsync(-1), model);
        }

        [TestMethod]
        public void DeleteValidPC()
        {
            var id = 9990;
            var result = controller.Delete(id);
            var viewResult = (ViewResult)result.Result;
            PC pc = (PC)viewResult.Model;

            Assert.AreEqual(pcs[0], pc);
        }

        [TestMethod]
        public void EditValidId()
        {
            var testPC = new PC { Id = 99900, CPU = "FakeCPU 1", GPU = "FakeGPU 1", MOBO = "FakeMOBO 1", User = new User { Id = 9990, Username = "randomName", Email = "random@email.com", Password = "123!random" } };
            var result = controller.Edit(0, testPC);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void DeleteReturnsToDelete()
        {
            var id = 9990;
            var result = controller.Delete(id);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Delete", viewResult.ViewName);
        }

        [TestMethod]
        public void DeleteReturnsToIndex()
        {
            var id = 9990;
            var result = controller.DeleteConfirmed(id);
            var actionResult = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index", actionResult.ActionName);
        }

        [TestMethod]
        public void DeleteValidId()
        {
            var id = 9990;
            var result = controller.DeleteConfirmed(id);
            var pc = _context.PC.Find(id);

            Assert.AreEqual(pc, null);
        }

        [TestMethod]
        public void DeleteInvalidId()
        {
            var result = controller.Delete(-1);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void DeleteNullId()
        {
            var result = controller.Delete(null);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void CheckIfPCExists()
        {
            var id = 9990;
            bool result = controller.PCExists(id);

            Assert.AreEqual(true, result);
        }
    }
}
