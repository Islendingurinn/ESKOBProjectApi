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
    public class IdeasController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateIdea createidea, string reference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var _context = new ESKOBDbContext();
            Tenant tenant = _context.Tenants.Where(t => t.Reference == reference).FirstOrDefault();
            if (tenant == null) return NotFound(reference);

            Idea idea = new Idea
            {
                Submitted = DateTime.Now,
                Last_Edit = DateTime.Now,
                Status = "NEW",
                TenantId = tenant.Id,
                Title = createidea.Title,
                Description = createidea.Description,
                Effort = createidea.Effort,
                Impact = createidea.Impact,
                Priority = createidea.Priority
            };

            await _context.Ideas.AddAsync(idea);
            await _context.SaveChangesAsync();

            return Ok(idea);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Edit([FromBody] EditIdea editidea, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var _context = new ESKOBDbContext();
            Idea toEdit = _context.Ideas.Where(i => i.Id == id).FirstOrDefault();
            if (toEdit == null) return NotFound(id);

            if (editidea.Cost_Save != null)
                toEdit.Cost_Save = editidea.Cost_Save;

            if (editidea.Challenges != null)
                toEdit.Challenges = editidea.Challenges;

            if (editidea.Results != null)
                toEdit.Results = editidea.Results;

            if (editidea.Priority != null)
                toEdit.Priority = editidea.Priority;

            await _context.SaveChangesAsync();

            return Ok(toEdit);
        }

        [HttpGet]
        [Route("{search?}")]
        public ActionResult Get(string search, string reference)
        {
            DateTime now = DateTime.Now;
            using var _context = new ESKOBDbContext();

            Tenant tenant =_context.Tenants.Where(t => t.Reference == reference).FirstOrDefault();
            if (tenant == null) return NotFound(reference);
            int tenantId = tenant.Id;

            if (search == null)
            {
                return Ok(_context.Ideas
                    .Include(idea => idea.Hashtags)
                    .Include(idea => idea.Comments)
                    .Where(idea => !idea.Status.ToLower().Equals("deleted")
                    && idea.TenantId == tenantId)
                    .OrderByDescending(idea => idea.Submitted)
                    .ToList());
            }

            switch (search.ToLower())
            {
                case ("day"):
                    return Ok(_context.Ideas.Include(idea => idea.Hashtags)
                        .Include(idea => idea.Comments)
                        .Where(idea => idea.Submitted >= now.AddHours(-24)
                        && !idea.Status.ToLower().Equals("deleted")
                        && idea.TenantId == tenantId)
                        .OrderByDescending(idea => idea.Submitted)
                        .ToList());
                case ("week"):
                    return Ok(_context.Ideas.Include(idea => idea.Hashtags)
                        .Include(idea => idea.Comments)
                        .Where(idea => idea.Submitted >= now.AddDays(-7)
                        && !idea.Status.ToLower().Equals("deleted")
                        && idea.TenantId == tenantId)
                        .OrderByDescending(idea => idea.Submitted)
                        .ToList());
                case ("month"):
                    return Ok(_context.Ideas.Include(idea => idea.Hashtags)
                        .Include(idea => idea.Comments)
                        .Where(idea => idea.Submitted >= now.AddMonths(-1)
                        && !idea.Status.ToLower().Equals("deleted")
                        && idea.TenantId == tenantId)
                        .OrderByDescending(idea => idea.Submitted)
                        .ToList());
                case ("year"):
                    return Ok(_context.Ideas.Include(idea => idea.Hashtags)
                        .Include(idea => idea.Comments)
                        .Where(idea => idea.Submitted >= now.AddYears(-1)
                        && !idea.Status.ToLower().Equals("deleted")
                        && idea.TenantId == tenantId)
                        .OrderByDescending(idea => idea.Submitted)
                        .ToList());
                case ("h"):
                    return Ok(_context.Ideas.Include(idea => idea.Hashtags)
                        .Include(idea => idea.Comments)
                        .Where(idea => idea.Priority.ToLower().Equals("h")
                        && !idea.Status.ToLower().Equals("deleted")
                        && idea.TenantId == tenantId)
                        .OrderByDescending(idea => idea.Submitted)
                        .ToList());
                case ("m"):
                    return Ok(_context.Ideas.Include(idea => idea.Hashtags)
                        .Include(idea => idea.Comments)
                        .Where(idea => idea.Priority.ToLower().Equals("m")
                        && !idea.Status.ToLower().Equals("deleted")
                        && idea.TenantId == tenantId)
                        .OrderByDescending(idea => idea.Submitted)
                        .ToList());
                case ("l"):
                    return Ok(_context.Ideas.Include(idea => idea.Hashtags)
                        .Include(idea => idea.Comments)
                        .Where(idea => idea.Priority.ToLower().Equals("l")
                        && !idea.Status.ToLower().Equals("deleted")
                        && idea.TenantId == tenantId)
                        .OrderByDescending(idea => idea.Submitted)
                        .ToList());
                case ("new"):
                    return Ok(_context.Ideas.Include(idea => idea.Hashtags)
                        .Include(idea => idea.Comments)
                        .Where(idea => idea.Status.ToLower().Equals("new")
                        && !idea.Status.ToLower().Equals("deleted")
                        && idea.TenantId == tenantId)
                        .OrderByDescending(idea => idea.Submitted)
                        .ToList());
                case ("under_review"):
                    return Ok(_context.Ideas.Include(idea => idea.Hashtags)
                        .Include(idea => idea.Comments)
                        .Where(idea => idea.Status.ToLower().Equals("under_review")
                        && !idea.Status.ToLower().Equals("deleted")
                        && idea.TenantId == tenantId)
                        .OrderByDescending(idea => idea.Submitted)
                        .ToList());
                case ("under_implementation"):
                    return Ok(_context.Ideas.Include(idea => idea.Hashtags)
                        .Include(idea => idea.Comments)
                        .Where(idea => idea.Status.ToLower().Equals("under_implementation")
                        && !idea.Status.ToLower().Equals("deleted")
                        && idea.TenantId == tenantId)
                        .OrderByDescending(idea => idea.Submitted)
                        .ToList());
                case ("implemented"):
                    return Ok(_context.Ideas.Include(idea => idea.Hashtags)
                        .Include(idea => idea.Comments)
                        .Where(idea => idea.Status.ToLower().Equals("implemented")
                        && !idea.Status.ToLower().Equals("deleted")
                        && idea.TenantId == tenantId)
                        .OrderByDescending(idea => idea.Submitted)
                        .ToList());
                default:
                    return Ok(_context.Ideas.Include(idea => idea.Hashtags)
                        .Include(idea => idea.Comments)
                        .Where(idea => idea.Title.ToLower().Contains(search.ToLower())
                        || idea.Description.ToLower().Contains(search.ToLower())
                        || idea.Hashtags.Any(hashtag => hashtag.Tag.ToLower().Contains(search.ToLower()))
                        && !idea.Status.ToLower().Equals("deleted")
                        && idea.TenantId == tenantId)
                        .OrderByDescending(idea => idea.Submitted)
                        .ToList());
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Get(int id)
        {
            using var _context = new ESKOBDbContext();
            
            Idea idea = _context.Ideas.Where(x => x.Id == id)
                .Include(idea => idea.Hashtags)
                .Include(idea => idea.Comments
                .OrderByDescending(comment => comment.Date))
                .ThenInclude(comment => comment.Author)
                .Include(idea => idea.Tasks.OrderBy(task => task.Id)
                .Where(task => !task.Status.Equals("DELETED")))
                .ThenInclude(task => task.Creator)
                .FirstOrDefault();

            if (idea == null) return NotFound(id);

            foreach(Comment c in idea.Comments)
            {
                if (c.Author == null)
                {
                    c.Author = new Manager { Id = -1, Name = "Undefined" };
                };
            }

            foreach (Models.Task t in idea.Tasks)
            {
                if (t.Creator == null)
                {
                    t.Creator = new Manager { Id = -1, Name = "Undefined" };
                }
            }

            return Ok(idea);
        }
        
        [HttpPut]
        [Route("{id:int}/{status}")]
        public async Task<ActionResult> Escalate(string status, int id)
        {
            using var _context = new ESKOBDbContext();

            Idea idea = _context.Ideas.Where(i => i.Id == id).FirstOrDefault();
            if (idea == null) return NotFound(id);

            String newStatus = "";
            switch (status.ToUpper())
            {
                case "UNDER_REVIEW":
                    if (idea.Status.ToLower().Equals("new"))
                    {
                        newStatus = status;
                    }
                    break;
                case "UNDER_IMPLEMENTATION":
                    if (idea.Status.ToLower().Equals("under_review"))
                    {
                        newStatus = status;
                    }
                    break;
                case "IMPLEMENTED":
                    if (idea.Status.ToLower().Equals("under_implementation"))
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
                return BadRequest(status + " does not come after " + idea.Status);

            idea.Status = newStatus.ToUpper();
            await _context.SaveChangesAsync();
            return Ok(idea);
        }
    }
}
