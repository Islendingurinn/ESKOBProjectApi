using System;
using System.Linq;
using System.Threading.Tasks;
using ESKOBApi.Models;
using ESKOBApi.Models.Comments;
using Microsoft.AspNetCore.Mvc;

namespace ESKOBApi.Controllers
{
    [ApiController]
    [Route("{reference}/[controller]")]
    public class CommentsController : ControllerBase
    {
      
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateComment newcomment, string reference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var _context = new ESKOBDbContext();
            Tenant tenant = _context.Tenants.Where(t => t.Reference == reference).FirstOrDefault();
            if (tenant == null) return NotFound(reference);

            Comment comment = new Comment
            {
                Date = DateTime.Now,
                TenantId = tenant.Id,
                Body = newcomment.Body,
                IdeaId = newcomment.IdeaId,
                TaskId = newcomment.TaskId
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }
    }
}
