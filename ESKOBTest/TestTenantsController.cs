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
    public class TestTenantsController
    {

        [TestMethod]
        public void GetTenants_ShouldListTenants()
        {
            var controller = new TenantsController();

            var actionresult =
                controller.Get(0);

            var result = actionresult as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsInstanceOfType(result.Value, typeof(List<Tenant>));
        }

        [TestMethod]
        public void GetTenant_ShouldReturnTenant()
        {
            var controller = new TenantsController();

            var actionresult =
                controller.Get(1);

            var result = actionresult as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsInstanceOfType(result.Value, typeof(Tenant));
            Assert.AreEqual(((Tenant)result.Value).Id, 1);
        }

        [TestMethod]
        public void GetTenant_NotFound()
        {
            var controller = new TenantsController();

            var actionresult =
                controller.Get(-1);

            var result = actionresult as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        [TestMethod]
        public void PostTenant_ShouldReturnTenant()
        {
            var controller = new TenantsController();

            var item = GetDemoCreateTenant();

            var actionresult =
                controller.Create(item);

            var result = actionresult.Result as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsInstanceOfType(result.Value, typeof(Tenant));
            Assert.AreEqual(((Tenant)result.Value).Reference, item.Reference);
        }

        [TestMethod]
        public void EditTenant_ShouldReturnSameTenant()
        {
            var controller = new TenantsController();

            var item = GetDemoEditTenant();

            var actionresult =
                controller.Edit(item, 3);

            var result = actionresult.Result as OkObjectResult;

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(((Tenant)result.Value).Name, item.Name);
        }

        [TestMethod]
        public void EditTenant_InvalidTenantId()
        {
            var controller = new TenantsController();

            var item = GetDemoEditTenant();

            var actionresult =
                controller.Edit(item, -1);

            var result = actionresult.Result as NotFoundObjectResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            Assert.AreEqual(result.StatusCode, 404);
        }

        CreateTenant GetDemoCreateTenant()
        {
            return new CreateTenant() { Name = "Unit Testing", Reference = "unit123" };
        }

        EditTenant GetDemoEditTenant()
        {
            return new EditTenant() { Name = "Edited Testing", Reference = "unit123" };
        }
    }
}
