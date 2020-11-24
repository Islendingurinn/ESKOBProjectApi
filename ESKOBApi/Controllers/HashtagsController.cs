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
    [Route("api/[controller]/[action]")]
    public class HashtagsController : ControllerBase
    {
        [HttpGet]
        [Route("{tag}")]
        public IEnumerable<Idea> GetIdeas(string tag)
        {
            using (var _context = new ESKOBDbContext())
            {
                return _context.Hashtags.Where(hashtag => hashtag.Tag == tag).Include(i => i.Idea.Hashtags).Select(idea => idea.Idea).ToList();
            }
        }

        [HttpPost]
        public HttpResponseMessage Create([FromBody] Hashtag hashtag)
        {
            using (var _context = new ESKOBDbContext())
            {
                _context.Hashtags.Add(hashtag);
                _context.SaveChanges();
            }

            return new HttpResponseMessage();
        }
    }    
}
