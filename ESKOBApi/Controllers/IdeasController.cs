using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ESKOBApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class IdeasController : ControllerBase
    {
        [HttpPost]
        public HttpResponseMessage Create([FromBody] Idea idea)
        {
            using (var _context = new ESKOBDbContext())
            {
                idea.Submitted = DateTime.Now;
                idea.Last_Edit = DateTime.Now;
                idea.Status = "NEW";
                _context.Ideas.Add(idea);
                _context.SaveChanges();

                return new HttpResponseMessage()
                {
                    ReasonPhrase = "" + idea.Id
                };
            }
        }

        [HttpGet]
        [Route("{parameter}/{search?}")]
        public IEnumerable<Idea> Get(string parameter, string search)
        {
            DateTime now = DateTime.Now;
            using(var _context = new ESKOBDbContext())
            {
                switch (parameter.ToLower())
                {
                    case ("day"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Submitted >= now.AddHours(-24) 
                            && !idea.Status.ToLower().Equals("deleted"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("week"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Submitted >= now.AddDays(-7)
                            && !idea.Status.ToLower().Equals("deleted"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("month"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Submitted >= now.AddMonths(-1)
                            && !idea.Status.ToLower().Equals("deleted"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("year"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Submitted >= now.AddYears(-1)
                            && !idea.Status.ToLower().Equals("deleted"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("h"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Priority.ToLower().Equals("h")
                            && !idea.Status.ToLower().Equals("deleted"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("m"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Priority.ToLower().Equals("m")
                            && !idea.Status.ToLower().Equals("deleted"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case("l"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Priority.ToLower().Equals("l")
                            && !idea.Status.ToLower().Equals("deleted"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("new"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Status.ToLower().Equals("new")
                            && !idea.Status.ToLower().Equals("deleted"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("under_review"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Status.ToLower().Equals("under_review")
                            && !idea.Status.ToLower().Equals("deleted"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("under_implementation"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Status.ToLower().Equals("under_implementation")
                            && !idea.Status.ToLower().Equals("deleted"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("implemented"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Status.ToLower().Equals("implemented")
                            && !idea.Status.ToLower().Equals("deleted"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("search"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Title.ToLower().Contains(search.ToLower())
                            || idea.Description.ToLower().Contains(search.ToLower())
                            || idea.Hashtags.Any(hashtag => hashtag.Tag.ToLower().Contains(search.ToLower()))
                            && !idea.Status.ToLower().Equals("deleted"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    default:
                        return _context.Ideas.Include(idea => idea.Hashtags)
                        .Where(idea => !idea.Status.ToLower().Equals("deleted")).
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
                return _context.Ideas.Where(x => x.Id == id)
                    .Include(idea => idea.Hashtags)
                    .Include(idea => idea.Comments
                    .OrderByDescending(comment => comment.Date))
                    .FirstOrDefault();
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
