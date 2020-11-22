using System;
using System.Collections.Generic;
using System.Linq;

namespace ESKOBApi.Models
{
    public class Added_User
    {
        public int Id { get; set; }
        public int IdeaId { get; set; }
        public int TaskId { get; set; }
        public int AddedId { get; set; }
        public int AdderId { get; set; }

        public virtual Idea Idea { get; set; }
        public virtual Task Task { get; set; }

        public virtual Manager Added { get; set; }
        public virtual Manager Adder { get; set; }
    }
}
