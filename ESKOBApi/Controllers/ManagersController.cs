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
    [Route("api/[controller]/[action]")]
    public class ManagersController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Manager> GetAll()
        {
            using(var _context = new ESKOBDbContext())
            {
                return _context.Managers.Select(manager =>
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
