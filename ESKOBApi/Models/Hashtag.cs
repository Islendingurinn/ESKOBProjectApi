using System;
using System.Collections.Generic;
using System.Linq;

namespace ESKOBApi.Models
{
    public class Hashtag
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public int IdeaId { get; set; }
        public int TenantId { get; set; }

        public virtual Idea Idea { get; set; }
        //public Task Task { get; set; }
        public virtual Tenant Tenant { get; set; }

    }
}
