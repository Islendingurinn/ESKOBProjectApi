using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESKOBApi.Models;
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
            if (tenant == null) return NotFound(reference);

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
            {
                return BadRequest(ModelState);
            }

            using var _context = new ESKOBDbContext();
            Tenant tenant = _context.Tenants.Where(t => t.Reference == reference).FirstOrDefault();
            if (tenant == null) return NotFound(reference);

            Hashtag hashtag = new Hashtag
            {
                Tag = createhashtag.Tag,
                IdeaId = createhashtag.IdeaId,
                TenantId = tenant.Id
            };

            await _context.Hashtags.AddAsync(hashtag);
            await _context.SaveChangesAsync();
            

            return Ok(hashtag);
        }
    }    
}
