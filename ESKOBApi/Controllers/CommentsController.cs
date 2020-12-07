using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using ESKOBApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ESKOBApi.Controllers
{
    [ApiController]
    [Route("{tenant}/[controller]/[action]")]
    public class CommentsController : ControllerBase
    {
      
        [HttpPost]
        public HttpResponseMessage Create([FromBody] Comment comment, string tenant)
        {
            using (var _context = new ESKOBDbContext())
            {
                comment.Date = DateTime.Now;
                comment.TenantId = _context.Tenants.Where(t => t.Reference == tenant).FirstOrDefault().Id;
                _context.Comments.Add(comment);
                _context.SaveChanges();

                return new HttpResponseMessage(){};
            }
        }
    }
}
