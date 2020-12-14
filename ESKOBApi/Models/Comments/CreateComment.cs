namespace ESKOBApi.Models.Comments
{
    public class CreateComment
    {
        public string Body { get; set; }
        public int IdeaId { get; set; }
        public int? TaskId { get; set; }
    }
}
