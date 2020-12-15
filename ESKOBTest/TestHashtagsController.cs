using ESKOBApi;
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
    public class TestHashtagsController
    {
        [TestMethod]
        public void GetIdeasFromHashtags_ShouldReturnList()
        {
            var controller = new HashtagsController();

            var actionresult =
                controller.GetIdeas("asdf", "ecco123");

            var result = actionresult.Result as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsInstanceOfType(result.Value, typeof(List<Idea>));
        }

        [TestMethod]
        public void GetIdeasFromHashtags_ShouldReturnInvalid()
        {
            var controller = new HashtagsController();

            var actionresult =
                controller.GetIdeas("asdf", "INVALIDREFERENCE");

            var result = actionresult.Result as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        [TestMethod]
        public void PostHashtag_ShouldReturnHashtag()
        {
            var controller = new HashtagsController();

            var item = GetDemoCreateHashtag();

            var actionresult =
                controller.Create(item, "ecco123");

            var result = actionresult.Result as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsInstanceOfType(result.Value, typeof(Hashtag));
            Assert.AreEqual(((Hashtag)result.Value).Tag, item.Tag);
        }

        [TestMethod]
        public void PostComment_ShouldReturnInvalid()
        {
            var controller = new HashtagsController();

            var item = GetDemoCreateHashtag();

            var actionresult =
                controller.Create(item, "INVALIDREFERENCE");

            var result = actionresult.Result as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        CreateHashtag GetDemoCreateHashtag()
        {
            return new CreateHashtag() { Tag = "Unit tested hashtag", IdeaId = 8 };
        }
    }
}
