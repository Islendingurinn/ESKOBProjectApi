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
    public class IdeasController : ControllerBase
    {
        [HttpPost]
        public HttpResponseMessage Create([FromBody] Idea idea, string tenant)
        {
            using (var _context = new ESKOBDbContext())
            {
                idea.Submitted = DateTime.Now;
                idea.Last_Edit = DateTime.Now;
                idea.Status = "NEW";

                idea.TenantId = _context.Tenants.Where(t => t.Reference == tenant).FirstOrDefault().Id;
                _context.Ideas.Add(idea);
                _context.SaveChanges();

                return new HttpResponseMessage()
                {
                    ReasonPhrase = "" + idea.Id
                };
            }
        }

        [HttpPost]
        public HttpResponseMessage Edit([FromBody] Idea idea)
        {
            using (var _context = new ESKOBDbContext())
            {
                Idea toEdit = _context.Ideas.Where(i => i.Id == idea.Id).FirstOrDefault();

                if (idea.Cost_Save != null)
                    toEdit.Cost_Save = idea.Cost_Save;

                if (idea.Challenges != null)
                    toEdit.Challenges = idea.Challenges;

                if (idea.Results != null)
                    toEdit.Results = idea.Results;

                _context.SaveChanges();

                return new HttpResponseMessage(){};
            }
        }

        [HttpGet]
        [Route("{parameter}/{search?}")]
        public IEnumerable<Idea> Get(string parameter, string search, string tenant)
        {
            DateTime now = DateTime.Now;
            using(var _context = new ESKOBDbContext())
            {
                int tenantId = _context.Tenants.Where(t => t.Reference == tenant).FirstOrDefault().Id; 
                switch (parameter.ToLower())
                {
                    case ("day"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Include(idea => idea.Comments)
                            .Where(idea => idea.Submitted >= now.AddHours(-24) 
                            && !idea.Status.ToLower().Equals("deleted")
                            && idea.TenantId == tenantId)
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("week"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Include(idea => idea.Comments)
                            .Where(idea => idea.Submitted >= now.AddDays(-7)
                            && !idea.Status.ToLower().Equals("deleted")
                            && idea.TenantId == tenantId)
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("month"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Include(idea => idea.Comments)
                            .Where(idea => idea.Submitted >= now.AddMonths(-1)
                            && !idea.Status.ToLower().Equals("deleted")
                            && idea.TenantId == tenantId)
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("year"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Include(idea => idea.Comments)
                            .Where(idea => idea.Submitted >= now.AddYears(-1)
                            && !idea.Status.ToLower().Equals("deleted")
                            && idea.TenantId == tenantId)
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("h"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Include(idea => idea.Comments)
                            .Where(idea => idea.Priority.ToLower().Equals("h")
                            && !idea.Status.ToLower().Equals("deleted")
                            && idea.TenantId == tenantId)
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("m"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Include(idea => idea.Comments)
                            .Where(idea => idea.Priority.ToLower().Equals("m")
                            && !idea.Status.ToLower().Equals("deleted")
                            && idea.TenantId == tenantId)
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case("l"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Include(idea => idea.Comments)
                            .Where(idea => idea.Priority.ToLower().Equals("l")
                            && !idea.Status.ToLower().Equals("deleted")
                            && idea.TenantId == tenantId)
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("new"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Include(idea => idea.Comments)
                            .Where(idea => idea.Status.ToLower().Equals("new")
                            && !idea.Status.ToLower().Equals("deleted")
                            && idea.TenantId == tenantId)
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("under_review"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Include(idea => idea.Comments)
                            .Where(idea => idea.Status.ToLower().Equals("under_review")
                            && !idea.Status.ToLower().Equals("deleted")
                            && idea.TenantId == tenantId)
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("under_implementation"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Include(idea => idea.Comments)
                            .Where(idea => idea.Status.ToLower().Equals("under_implementation")
                            && !idea.Status.ToLower().Equals("deleted")
                            && idea.TenantId == tenantId)
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("implemented"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Include(idea => idea.Comments)
                            .Where(idea => idea.Status.ToLower().Equals("implemented")
                            && !idea.Status.ToLower().Equals("deleted")
                            && idea.TenantId == tenantId)
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("search"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Include(idea => idea.Comments)
                            .Where(idea => idea.Title.ToLower().Contains(search.ToLower())
                            || idea.Description.ToLower().Contains(search.ToLower())
                            || idea.Hashtags.Any(hashtag => hashtag.Tag.ToLower().Contains(search.ToLower()))
                            && !idea.Status.ToLower().Equals("deleted")
                            && idea.TenantId == tenantId)
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    default:
                        return _context.Ideas.Include(idea => idea.Hashtags)
                        .Include(idea => idea.Comments)
                        .Where(idea => !idea.Status.ToLower().Equals("deleted")
                        && idea.TenantId == tenantId).
                        OrderByDescending(idea => idea.Submitted).ToList();
                }
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public Idea Get(int id)
        {
            using (var _context = new ESKOBDbContext())
            {
                Idea idea = _context.Ideas.Where(x => x.Id == id)
                    .Include(idea => idea.Hashtags)
                    .Include(idea => idea.Comments
                    .OrderByDescending(comment => comment.Date))
                    .ThenInclude(comment => comment.Author)
                    .Include(idea => idea.Tasks.OrderBy(task => task.Id)
                    .Where(task => !task.Status.Equals("DELETED")))
                    .ThenInclude(task => task.Creator)
                    .FirstOrDefault();
                foreach(Comment c in idea.Comments)
                {
                    if (c.Author == null)
                    {
                        c.Author = new Manager { Id = -1, Name = "Undefined" };
                    };
                    c.Author.Password = string.Empty;
                }
                foreach (Task t in idea.Tasks)
                {
                    if (t.Creator == null)
                    {
                        t.Creator = new Manager { Id = -1, Name = "Undefined" };
                    }
                    t.Creator.Password = string.Empty;
                }

                return idea;
            }
        }

        [HttpGet]
        [Route("{status}/{id:int}")]
        public void Escalate(string status, int id)
        {
            using(var _context = new ESKOBDbContext())
            {
                Idea idea = _context.Ideas.Where(i => i.Id == id).FirstOrDefault();
                switch (status.ToUpper())
                {
                    case "UNDER_REVIEW":
                        if (idea.Status.ToLower().Equals("new"))
                        {
                            idea.Status = status;
                            _context.SaveChanges();
                        }
                        break;
                    case "UNDER_IMPLEMENTATION":
                        if (idea.Status.ToLower().Equals("under_review"))
                        {
                            idea.Status = status;
                            _context.SaveChanges();
                        }
                        break;
                    case "IMPLEMENTED":
                        if (idea.Status.ToLower().Equals("under_implementation"))
                        {
                            idea.Status = status;
                            _context.SaveChanges();
                        }
                        break;
                    case "DELETED":
                        idea.Status = status;
                        _context.SaveChanges();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
