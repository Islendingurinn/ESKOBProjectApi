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

        [HttpGet]
        public IEnumerable<Manager> GetAll(string tenant)
        {
            using(var _context = new ESKOBDbContext())
            {
                int tenantId = _context.Tenants.Where(t => t.Reference == tenant).FirstOrDefault().Id;
                return _context.Managers
                    .Where(manager => manager.TenantId == tenantId)
                    .Select(manager =>
                new Manager
                {
                    Id = manager.Id,
                    Name = manager.Name,
                    Type = manager.Type
                }
                ).ToList();
            }
        }
    }
}
