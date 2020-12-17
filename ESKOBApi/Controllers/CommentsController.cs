using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ESKOBApi.Models;
using ESKOBApi.Models.Comments;
using ESKOBApi.Models.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                TaskId = newcomment.TaskId,
                AuthorId = newcomment.AuthorId
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            comment = _context.Comments
                .Where(c => c.Id == comment.Id)
                .Include(c => c.Author)
                .FirstOrDefault();
            foreach(String s in comment.Body.Split(" "))
            {
                if (!Regex.IsMatch(s, @"^@\[[a-zA-z]{2,}\]\(Manager:\d{1,}\)*")) continue;

                int id = Int32.Parse(s.Replace(")", "").Split(":").Last());
                string link = "";
                if(comment.TaskId == 0)
                {
                    link = "ideas/idea/" + comment.IdeaId;
                }
                else
                {
                    link = "tasks/task/" + comment.TaskId;
                }
                Notification notif = new Notification()
                {
                    Title = "Mention",
                    Description = comment.Author.Name + " mentioned you!",
                    Link = link,
                    ManagerId = id
                };

                await _context.AddAsync(notif);
            }
            await _context.SaveChangesAsync();

            return Ok(comment);
        }
    }
}
