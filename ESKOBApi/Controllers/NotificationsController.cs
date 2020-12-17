using System.Linq;
using System.Threading.Tasks;
using ESKOBApi.Models;
using ESKOBApi.Models.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace ESKOBApi.Controllers
{
    [ApiController]
    [Route("{reference}/[controller]")]
    public class NotificationsController : ControllerBase
    {
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Get(int id)
        {
            using var _context = new ESKOBDbContext();
            return Ok(_context.Notifications
                .Where(n => n.ManagerId == id)
                .OrderByDescending(n => n.Id).ToList());
        }

        [HttpPost]
        [Route("{id:int}/{tag}")]
        public async Task<ActionResult> Subscribe(int id, string tag)
        {
            using var _context = new ESKOBDbContext();
            Subscription sub = new Subscription()
            {
                ManagerId = id,
                Tag = tag
            };

            await _context.Subscriptions.AddAsync(sub);
            await _context.SaveChangesAsync();

            return Ok(sub);
        }
    }
}
