using ESKOBApi.Models.Comments;
using System;
using System.Collections.Generic;
namespace ESKOBApi.Models
{
    public class Comment : CreateComment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public int? IdeaId { get; set; }
        public int? TaskId { get; set; }
        public int? AuthorId { get; set; }
        public int TenantId { get; set; }

        public virtual Manager Author { get; set; }
    }
}
