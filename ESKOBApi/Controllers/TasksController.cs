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
    public class TasksController : ControllerBase
    {
        [HttpPost]
        public HttpResponseMessage Create([FromBody] Task task, string tenant)
        {
            using (var _context = new ESKOBDbContext())
            {
                task.Status = "NOT_STARTED";
                task.TenantId = _context.Tenants.Where(t => t.Reference == tenant).FirstOrDefault().Id;
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return new HttpResponseMessage(){};
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public Task Get(int id)
        {
            using (var _context = new ESKOBDbContext())
            {
                Task task = _context.Tasks.Where(x => x.Id == id)
                    .Include(tasks => tasks.Idea)
                    .Include(tasks => tasks.Creator)
                    .Include(tasks => tasks.Comments).FirstOrDefault();

                if(task.Creator == null)
                {
                    task.Creator = new Manager { Id = -1, Name = "Undefined" };
                }
                task.Creator.Password = String.Empty;
                return task;
            }
        }

        [HttpGet]
        [Route("{status}/{id:int}")]
        public void Escalate(string status, int id)
        {
            using(var _context = new ESKOBDbContext())
            {
                Task task = _context.Tasks.Where(i => i.Id == id).FirstOrDefault();
                switch (status.ToUpper())
                {
                    case "IN_PROGRESS":
                        if (task.Status.ToLower().Equals("not_started"))
                        {
                            task.Status = status;
                            _context.SaveChanges();
                        }
                        break;
                    case "DONE":
                        if (task.Status.ToLower().Equals("in_progress"))
                        {
                            task.Status = status;
                            _context.SaveChanges();
                        }
                        break;
                    case "DELETED":
                        task.Status = status;
                        _context.SaveChanges();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
