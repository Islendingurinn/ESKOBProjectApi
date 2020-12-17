using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESKOBApi.Models;
using ESKOBApi.Models.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKOBApi.Controllers
{
    [ApiController]
    [Route("{reference}/[controller]")]
    public class HashtagsController : ControllerBase
    {
        [HttpGet]
        [Route("{tag}")]
        public ActionResult<IEnumerable<Idea>> GetIdeas(string tag, string reference)
        {
            using var _context = new ESKOBDbContext();

            Tenant tenant = _context.Tenants.Where(t => t.Reference == reference).FirstOrDefault();
            if (tenant == null) 
                return NotFound(reference);

            return Ok(_context.Hashtags
                .Where(hashtag => hashtag.Tag == tag && hashtag.TenantId == tenant.Id)
                .Include(i => i.Idea.Hashtags)
                .Select(idea => idea.Idea)
                .ToList());
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateHashtag createhashtag, string reference)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using var _context = new ESKOBDbContext();
            Tenant tenant = _context.Tenants.Where(t => t.Reference == reference).FirstOrDefault();
            if (tenant == null) 
                return NotFound(reference);

            Hashtag hashtag = new Hashtag
            {
                Tag = createhashtag.Tag,
                IdeaId = createhashtag.IdeaId,
                TenantId = tenant.Id
            };

            await _context.Hashtags.AddAsync(hashtag);
            await _context.SaveChangesAsync();

            hashtag = _context.Hashtags
                .Where(h => h.Id == hashtag.Id)
                .Include(h => h.Idea)
                .FirstOrDefault();

            List<Subscription> subs = _context.Subscriptions
                .Where(s => s.Tag.ToLower().Equals(hashtag.Tag.ToLower())).ToList();

            foreach(Subscription s in subs)
            {
                Notification notif = new Notification()
                {
                    Title = "Subscription",
                    Description = hashtag.Idea.Title,
                    Link = "ideas/idea/" + hashtag.IdeaId,
                    ManagerId = s.ManagerId
                };

                await _context.AddAsync(notif);
            }
            await _context.SaveChangesAsync();

            return Ok(hashtag);
        }
    }    
}
