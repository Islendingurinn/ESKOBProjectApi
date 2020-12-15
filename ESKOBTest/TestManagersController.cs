using ESKOBApi.Controllers;
using ESKOBApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESKOBTest
{
    [TestClass]
    public class TestManagersController
    {
        [TestMethod]
        public void PostManager_ShouldReturnDummy()
        {
            var controller = new ManagersController();

            var item = GetDemoCreateManager();

            var actionresult =
                controller.Create(item, "ecco123");

            var result = actionresult.Result as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsInstanceOfType(result.Value, typeof(DummyManager));
            Assert.AreEqual(((DummyManager)result.Value).Name, item.Name);
        }

        [TestMethod]
        public void PostComment_ShouldReturnInvalid()
        {
            var controller = new ManagersController();

            var item = GetDemoCreateManager();

            var actionresult =
                controller.Create(item, "INVALIDREFERENCE");

            var result = actionresult.Result as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        [TestMethod]
        public void EditManager_ShouldReturnSameManager()
        {
            var controller = new ManagersController();

            var item = GetDemoEditManager();

            var actionresult =
                controller.Edit(item, 4);

            var result = actionresult.Result as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(((DummyManager)result.Value).Name, item.Name);
        }

        [TestMethod]
        public void EditIdea_InvalidIdeaId()
        {
            var controller = new ManagersController();

            var item = GetDemoEditManager();

            var actionresult =
                controller.Edit(item, -1);

            var result = actionresult.Result as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        [TestMethod]
        public void GetManagers_ShouldReturnList()
        {
            var controller = new ManagersController();

            var actionresult =
                controller.GetAll("ecco123");

            var result = actionresult as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsInstanceOfType(result.Value, typeof(List<DummyManager>));
        }

        [TestMethod]
        public void GetIdeas_InvalidReference()
        {
            var controller = new ManagersController();

            var actionresult =
                controller.GetAll("INVALIDreference");

            var result = actionresult as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        CreateManager GetDemoCreateManager()
        {
            return new CreateManager() { Name = "unittest1", Password = "asdf123" };
        }

        EditManager GetDemoEditManager()
        {
            return new EditManager() { Name = "edittest1" };
        }
    }
}
