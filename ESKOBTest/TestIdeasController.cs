using ESKOBApi;
using ESKOBApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http.Results;

namespace ESKOBTest
{
    [TestClass]
    public class TestIdeasController
    {
        [TestMethod]
        public void PostIdea_ShouldReturnSameIdea()
        {
            var controller = new IdeasController();

            var item = GetDemoCreateIdea();

            var actionresult =
                controller.Create(item, "ecco123");

            var result = actionresult.Result as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(((Idea)result.Value).Title, item.Title);
        }

        [TestMethod]
        public void PostIdea_InvalidReference()
        {
            var controller = new IdeasController();

            var item = GetDemoCreateIdea();

            var actionresult =
                controller.Create(item, "invalidREFRENCE");

            var result = actionresult.Result as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        [TestMethod]
        public void EditIdea_ShouldReturnSameIdea()
        {
            var controller = new IdeasController();

            var item = GetDemoEditIdea();

            var actionresult =
                controller.Edit(item, 8);

            var result = actionresult.Result as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(((Idea)result.Value).Priority, item.Priority);
        }

        [TestMethod]
        public void EditIdea_InvalidIdeaId()
        {
            var controller = new IdeasController();

            var item = GetDemoEditIdea();

            var actionresult =
                controller.Edit(item, -1);

            var result = actionresult.Result as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        [TestMethod]
        public void GetIdeas_ShouldReturnList()
        {
            var controller = new IdeasController();

            var actionresult =
                controller.Get("", "ecco123");

            var result = actionresult as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsInstanceOfType(result.Value, typeof(List<Idea>));
        }

        [TestMethod]
        public void GetIdeas_InvalidReference()
        {
            var controller = new IdeasController();

            var actionresult =
                controller.Get("h", "INVALIDreference");

            var result = actionresult as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        [TestMethod]
        public void GetIdea_ShouldReturnIdea()
        {
            var controller = new IdeasController();

            var actionresult =
                controller.Get(8);

            var result = actionresult as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(((Idea)result.Value).Id, 8);
        }

        [TestMethod]
        public void GetIdea_InvalidIdeaId()
        {
            var controller = new IdeasController();

            var actionresult =
                controller.Get(-1);

            var result = actionresult as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        [TestMethod]
        public void EscalateIdea_BadRequest()
        {
            var controller = new IdeasController();

            var actionresult =
                controller.Escalate("NEW", 8);

            var result = actionresult.Result as BadRequestObjectResult;

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual(result.StatusCode, 400);
        }

        CreateIdea GetDemoCreateIdea()
        {
            return new CreateIdea() { Title = "Demo Idea", Description = "A demo" };
        }

        EditIdea GetDemoEditIdea()
        {
            return new EditIdea() { Priority = "M", Challenges = "Unit testing demo!" };
        }
    }
}
