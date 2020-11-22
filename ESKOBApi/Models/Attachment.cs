using System;
using System.Collections.Generic;
using System.Linq;

namespace ESKOBApi.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int IdeaId { get; set; }
        public int TaskId { get; set; }

        public virtual Idea Idea { get; set; }
        public virtual Task Task { get; set; }
    }
}
