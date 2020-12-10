using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ESKOBApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKOBApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TenantsController : ControllerBase
    {
        [HttpGet]
        [Route("{reference}")]
        public bool Exists(string reference)
        {
            using (var _context = new ESKOBDbContext())
            {
                if (_context.Tenants.Where(t => t.Reference == reference).FirstOrDefault() != null)
                    return true;
            }

            return false;
        }

        [HttpGet]
        public List<Tenant> Get()
        {
            using (var _context = new ESKOBDbContext())
            {
                return _context.Tenants.ToList();
            }
        }

        [HttpPost]
        public HttpResponseMessage Create([FromBody] Tenant tenant)
        {
            using (var _context = new ESKOBDbContext())
            {
                _context.Tenants.Add(tenant);
                _context.SaveChanges();
            }

            return new HttpResponseMessage();
        }

        [HttpPost]
        public HttpResponseMessage Edit([FromBody] Tenant tenant)
        {
            using (var _context = new ESKOBDbContext())
            {
                Tenant edit = _context.Tenants.Where(t => t.Id == tenant.Id).FirstOrDefault();
                edit.Name = tenant.Name;
                edit.Reference = tenant.Reference;
                _context.SaveChanges();
            }

            return new HttpResponseMessage();
        }

        [HttpGet]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            using (var _context = new ESKOBDbContext())
            {
                Tenant tenant = _context.Tenants.Where(t => t.Id == id).FirstOrDefault();
                _context.Tenants.Remove(tenant);
                _context.SaveChanges();
            }
        }
    }    
}
