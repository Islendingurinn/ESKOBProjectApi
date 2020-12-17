using System.Linq;
using System.Threading.Tasks;
using ESKOBApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKOBApi.Controllers
{
    [ApiController]
    [Route("{reference}/[controller]")]
    public class ManagersController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateManager createmanager, string reference)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            using var _context = new ESKOBDbContext();
            Tenant tenant = _context.Tenants.Where(t => t.Reference == reference).FirstOrDefault();
            if (tenant == null) 
                return NotFound(reference);

            string password = string.IsNullOrEmpty(createmanager.Password) ? 
                createmanager.Name : createmanager.Password;

            string hashedPassword = PasswordEncryption.Hash(password);
            Manager manager = new Manager
            {
                Name = createmanager.Name,
                Password = hashedPassword,
                TenantId = tenant.Id
            };

            await _context.Managers.AddAsync(manager);
            await _context.SaveChangesAsync();

            DummyManager dummy = new DummyManager
            {
                Id = manager.Id,
                Name = manager.Name,
                TenantId = manager.TenantId
            };

            return Ok(dummy);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Edit([FromBody] EditManager editmanager, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using var _context = new ESKOBDbContext();
            Manager manager = _context.Managers.Where(m => m.Id == id).FirstOrDefault();
            if (manager == null) 
                return NotFound(id);

            if(!string.IsNullOrEmpty(editmanager.Name))
                manager.Name = editmanager.Name;

            if (!string.IsNullOrEmpty(editmanager.Password))
            {
                string hashedPassword = PasswordEncryption.Hash(editmanager.Password);
                manager.Password = hashedPassword;
            }
                
            await _context.SaveChangesAsync();

            DummyManager dummy = new DummyManager
            {
                Id = manager.Id,
                Name = manager.Name,
                TenantId = manager.TenantId
            };

            return Ok(dummy);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            using var _context = new ESKOBDbContext();
            Manager manager = _context.Managers.Where(m => m.Id == id).FirstOrDefault();
            if (manager == null) 
                return NotFound(id);

            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public ActionResult GetAll(string reference)
        {
            using var _context = new ESKOBDbContext();
            Tenant tenant = _context.Tenants.Where(t => t.Reference.Equals(reference)).FirstOrDefault();
            if (tenant == null) 
                return NotFound(reference);

            return Ok(_context.Managers
                .Where(manager => manager.TenantId == tenant.Id)
                .Select(manager =>
                new DummyManager
                {
                    Id = manager.Id,
                    Name = manager.Name,
                    TenantId = manager.TenantId
                }).ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult Get(int id)
        {
            using var _context = new ESKOBDbContext();

            Manager manager = _context.Managers
                .Where(manager => manager.Id == id)
                .FirstOrDefault();

            return Ok(new DummyManager()
            {
                Id = manager.Id,
                Name = manager.Name,
                TenantId = manager.TenantId
            });
        }
    }
}
