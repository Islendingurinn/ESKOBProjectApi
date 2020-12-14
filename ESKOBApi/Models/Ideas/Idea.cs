using ESKOBApi.Models;
using System;
using System.Collections.Generic; 

namespace ESKOBApi
{
    public class Idea : CreateIdea
    {
        public int Id { get; set; }
        public DateTime Submitted { get; set; }
        public DateTime Last_Edit { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Cost_Save { get; set; }
        public string Challenges { get; set; }
        public string Results { get; set; }
        public int Impact { get; set; }
        public int Effort { get; set; }
        public int TenantId { get; set; }

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
        public virtual ICollection<Hashtag> Hashtags { get; set; } = new List<Hashtag>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
