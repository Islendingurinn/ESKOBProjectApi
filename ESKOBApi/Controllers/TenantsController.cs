using System.Linq;
using System.Threading.Tasks;
using ESKOBApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKOBApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TenantsController : ControllerBase
    {

        [HttpGet]
        [Route("{id?:int}")]
        public ActionResult Get(int id)
        {
            using var _context = new ESKOBDbContext();
            if(id == 0)
            {
                return Ok(_context.Tenants.ToList());
            }
            else
            {
                Tenant tenant = _context.Tenants.Where(t => t.Id == id).FirstOrDefault();
                if (tenant == null) return NotFound(id);

                return Ok(tenant);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateTenant createtenant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var _context = new ESKOBDbContext();
            Tenant tenant = new Tenant
            {
                Name = createtenant.Name,
                Reference = createtenant.Reference
            };
            
            await _context.Tenants.AddAsync(tenant);
            await _context.SaveChangesAsync();
            
            return Ok(tenant);
        }

        [HttpPut]
        [Route("{int:id}")]
        public async Task<ActionResult> Edit([FromBody] EditTenant edittenant, int id)
        {
            using var _context = new ESKOBDbContext();
            Tenant tenant = _context.Tenants.Where(t => t.Id == id).FirstOrDefault();
            if (tenant == null) return NotFound(id);

            if(edittenant.Name != null)
                tenant.Name = edittenant.Name;

            if(edittenant.Reference != null)
                tenant.Reference = edittenant.Reference;

            await _context.SaveChangesAsync();
            return Ok(tenant);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            using var _context = new ESKOBDbContext();
            Tenant tenant = _context.Tenants.Where(t => t.Id == id).FirstOrDefault();
            if (tenant == null) return NotFound(id);

            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }    
}
