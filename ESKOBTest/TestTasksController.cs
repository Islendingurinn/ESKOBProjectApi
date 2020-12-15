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
    public class TestTasksController
    {
        [TestMethod]
        public void PostComment_ShouldReturnComment()
        {
            var controller = new TasksController();

            var item = GetDemoCreateTask();

            var actionresult =
                controller.Create(item, "ecco123");

            var result = actionresult.Result as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsInstanceOfType(result.Value, typeof(Task));
            Assert.AreEqual(((Task)result.Value).Title, item.Title);
        }

        [TestMethod]
        public void PostComment_ShouldReturnInvalid()
        {
            var controller = new TasksController();

            var item = GetDemoCreateTask();

            var actionresult =
                controller.Create(item, "INVALIDREFERENCE");

            var result = actionresult.Result as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        [TestMethod]
        public void GetTask_ShouldReturnTask()
        {
            var controller = new TasksController();

            var actionresult =
                controller.Get(2);

            var result = actionresult as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(((Task)result.Value).Id, 2);
        }

        [TestMethod]
        public void GetTask_InvalidTaskId()
        {
            var controller = new TasksController();

            var actionresult =
                controller.Get(-1);

            var result = actionresult as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        [TestMethod]
        public void EscalateTask_BadRequest()
        {
            var controller = new TasksController();

            var actionresult =
                controller.Escalate("NEW", 2);

            var result = actionresult.Result as BadRequestObjectResult;

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual(result.StatusCode, 400);
        }

        CreateTask GetDemoCreateTask()
        {
            return new CreateTask() { Title="From unit testing!", Description="Unit Tests", IdeaId=8, Estimation="1W" };
        }
    }
}
