using ESKOBApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;  

namespace ESKOBApi
{
    public class CreateIdea
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public int Impact { get; set; }
        public int Effort { get; set; }
        public string Employee { get; set; }
    }
}
