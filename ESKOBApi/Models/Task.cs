using System;
using System.Collections.Generic;
using System.Linq;

namespace ESKOBApi.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Estimation { get; set; }
        public int CreatorId { get; set; }
        public int IdeaId { get; set; }

        public virtual Manager Creator { get; set; }
        public virtual Idea Idea { get; set; }
    }
}
