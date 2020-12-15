using ESKOBApi.Controllers;
using ESKOBApi.Models;
using ESKOBApi.Models.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESKOBTest
{
    [TestClass]
    public class TestCommentsController
    {
        [TestMethod]
        public void PostComment_ShouldReturnComment()
        {
            var controller = new CommentsController();

            var item = GetDemoCreateComment();

            var actionresult =
                controller.Create(item, "ecco123");

            var result = actionresult.Result as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsInstanceOfType(result.Value, typeof(Comment));
            Assert.AreEqual(((Comment)result.Value).Body, item.Body);
        }

        [TestMethod]
        public void PostComment_ShouldReturnInvalid()
        {
            var controller = new CommentsController();

            var item = GetDemoCreateComment();

            var actionresult =
                controller.Create(item, "INVALIDREFERENCE");

            var result = actionresult.Result as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        CreateComment GetDemoCreateComment()
        {
            return new CreateComment() { Body = "Unit tested comment", IdeaId = 8 };
        }
    }
}
