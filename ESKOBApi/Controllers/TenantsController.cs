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
    }    
}
