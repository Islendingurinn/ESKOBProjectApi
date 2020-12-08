using System;
using System.Collections.Generic;
using System.Linq;

namespace ESKOBApi.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }
        public int? TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
