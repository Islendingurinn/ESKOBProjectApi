namespace ESKOBApi.Models
{
    public class Hashtag : CreateHashtag
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public int IdeaId { get; set; }
        public int TenantId { get; set; }

        public virtual Idea Idea { get; set; }
    }
}
