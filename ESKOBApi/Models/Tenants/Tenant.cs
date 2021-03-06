﻿using System.Collections.Generic;

namespace ESKOBApi.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }

        public virtual ICollection<Manager> Managers { get; set; } = new List<Manager>();
    }
}
