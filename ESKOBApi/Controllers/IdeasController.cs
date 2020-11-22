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
        private readonly ILogger<IdeasController> _logger;

        public IdeasController(ILogger<IdeasController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public HttpResponseMessage Create([FromBody] Idea idea)
        {
            using (var _context = new ESKOBDbContext())
            {
                idea.Submitted = DateTime.Now;
                idea.Last_Edit = DateTime.Now;
                _context.Ideas.Add(idea);
                _context.SaveChanges();
            }
            return new HttpResponseMessage();
        }

        [HttpGet]
        [Route("{parameter}")]
        public IEnumerable<Idea> Get(string parameter)
        {
            DateTime now = DateTime.Now;
            using(var _context = new ESKOBDbContext())
            {
                switch (parameter.ToLower())
                {
                    case ("day"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Submitted >= now.AddHours(-24))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("week"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Submitted >= now.AddDays(-7))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("month"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Submitted >= now.AddMonths(-1))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("year"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Submitted >= now.AddYears(-1))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("h"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Priority.ToLower().Equals("h"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("m"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Priority.ToLower().Equals("m"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case("l"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Priority.ToLower().Equals("l"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("new"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Status.ToLower().Equals("new"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("progress"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Status.ToLower().Equals("in_progress"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    case ("implemented"):
                        return _context.Ideas.Include(idea => idea.Hashtags)
                            .Where(idea => idea.Status.ToLower().Equals("implemented"))
                            .OrderByDescending(idea => idea.Submitted)
                            .ToList();
                    default:
                        return _context.Ideas.Include(idea => idea.Hashtags).OrderByDescending(idea => idea.Submitted).ToList();
                }
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public Idea Get(int id)
        {
            using (var _context = new ESKOBDbContext())
            {
                return _context.Ideas.Where(x => x.Id == id).Include(idea => idea.Hashtags).FirstOrDefault();
            }
        }
    }
}
