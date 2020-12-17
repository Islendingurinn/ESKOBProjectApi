namespace ESKOBApi.Models
{
    public class CreateTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Estimation { get; set; }
        public int IdeaId { get; set; }
        public int CreatorId { get; set; }
    }
}
