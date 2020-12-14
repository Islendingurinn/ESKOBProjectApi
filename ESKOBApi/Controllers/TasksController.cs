using System;
using System.Linq;
using System.Threading.Tasks;
using ESKOBApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKOBApi.Controllers
{
    [ApiController]
    [Route("{reference}/[controller]")]
    public class TasksController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateTask createtask, string reference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var _context = new ESKOBDbContext();
            Tenant tenant = _context.Tenants.Where(t => t.Reference == reference).FirstOrDefault();
            if (tenant == null) return NotFound(reference);

            Models.Task task = new Models.Task
            {
                Status = "NOT_STARTED",
                TenantId = tenant.Id,
                Title = createtask.Title,
                Description = createtask.Description,
                Estimation = createtask.Estimation,
                IdeaId = createtask.IdeaId
            };

            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Get(int id)
        {
            using var _context = new ESKOBDbContext();

            Models.Task task = _context.Tasks.Where(x => x.Id == id)
                .Include(tasks => tasks.Idea)
                .Include(tasks => tasks.Creator)
                .Include(tasks => tasks.Comments).FirstOrDefault();

            if (task == null) return NotFound(id);

            if (task.Creator == null)
            {
                task.Creator = new Manager { Id = -1, Name = "Undefined" };
            }

            return Ok(task);
        }

        [HttpPut]
        [Route("{id:int}/{status}")]
        public async Task<ActionResult> Escalate(string status, int id)
        {
            using var _context = new ESKOBDbContext();
            Models.Task task = _context.Tasks.Where(i => i.Id == id).FirstOrDefault();
            if (task == null) return NotFound(id);

            String newStatus = "";
            switch (status.ToUpper())
            {
                case "IN_PROGRESS":
                    if (task.Status.ToLower().Equals("not_started"))
                    {
                        newStatus = status;
                    }
                    break;
                case "DONE":
                    if (task.Status.ToLower().Equals("in_progress"))
                    {
                        newStatus = status;
                    }
                    break;
                case "DELETED":
                    newStatus = status;
                    break;
                default:
                    return BadRequest(status);
            }

            if (newStatus.Equals(""))
                return BadRequest(status + " does not come after " + task.Status);

            task.Status = newStatus;
            await _context.SaveChangesAsync();
            return Ok(task);
        }
    }
}
