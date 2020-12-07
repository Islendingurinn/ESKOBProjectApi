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
    [Route("{tenant}/[controller]/[action]")]
    public class HashtagsController : ControllerBase
    {
        [HttpGet]
        [Route("{tag}")]
        public IEnumerable<Idea> GetIdeas(string tag, string tenant)
        {
            using (var _context = new ESKOBDbContext())
            {
                int tenantId = _context.Tenants.Where(t => t.Reference == tenant).FirstOrDefault().Id;
                return _context.Hashtags.Where(hashtag => hashtag.Tag == tag && hashtag.TenantId == tenantId).Include(i => i.Idea.Hashtags).Select(idea => idea.Idea).ToList();
            }
        }

        [HttpPost]
        public HttpResponseMessage Create([FromBody] Hashtag hashtag, string tenant)
        {
            using (var _context = new ESKOBDbContext())
            {
                hashtag.TenantId = _context.Tenants.Where(t => t.Reference == tenant).FirstOrDefault().Id;
                _context.Hashtags.Add(hashtag);
                _context.SaveChanges();
            }

            return new HttpResponseMessage();
        }
    }    
}
