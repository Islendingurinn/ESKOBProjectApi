using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using ESKOBApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ESKOBApi.Controllers
{
    [ApiController]
    [Route("{tenant}/[controller]/[action]")]
    public class ManagersController : ControllerBase
    {

        [HttpPost]
        public HttpResponseMessage Create([FromBody] Manager manager, string tenant)
        {
            using (var _context = new ESKOBDbContext())
            {
                //todo: password
                manager.Password = manager.Name;

                _context.Managers.Add(manager);
                _context.SaveChanges();

                return new HttpResponseMessage();
            }
        }

        [HttpPost]
        public HttpResponseMessage Edit([FromBody] Manager manager)
        {
            using (var _context = new ESKOBDbContext())
            {
                Manager edit = _context.Managers.Where(m => m.Id == manager.Id).FirstOrDefault();
                edit.Name = manager.Name;
                _context.SaveChanges();

                return new HttpResponseMessage();
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            using(var _context = new ESKOBDbContext())
            {
                Manager manager = _context.Managers.Where(m => m.Id == id).FirstOrDefault();
                _context.Managers.Remove(manager);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Manager> GetAll(string tenant)
        {
            using(var _context = new ESKOBDbContext())
            {
                int tenantId = _context.Tenants.Where(t => t.Reference.Equals(tenant)).FirstOrDefault().Id;
                return _context.Managers
                    .Where(manager => manager.TenantId == tenantId)
                    .Select(manager =>
                new Manager
                {
                    Id = manager.Id,
                    Name = manager.Name,
                    TenantId = manager.TenantId
                }
                ).ToList();
            }
        }
    }
}
