using System.Linq;
using System.Threading.Tasks;
using ESKOBApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKOBApi.Controllers
{
    [ApiController]
    [Route("{reference}/[controller]")]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        public ActionResult Validate([FromBody] LoginManager loginmanager, string reference)
        {
            using var _context = new ESKOBDbContext();
            Tenant tenant = _context.Tenants.Where(t => t.Reference.Equals(reference)).FirstOrDefault();
            if (tenant == null) return NotFound(reference);

            Manager manager = _context.Managers
                .Where(manager => manager.Name.Equals(loginmanager.Name)
                && manager.Password.Equals(loginmanager.Password))
                .FirstOrDefault();

            if (manager == null) return NotFound(loginmanager.Name);
            else return Ok(new DummyManager { Id = manager.Id, Name = manager.Name, TenantId = manager.TenantId });

        }
    }    
}
