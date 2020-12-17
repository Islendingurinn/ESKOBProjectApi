namespace ESKOBApi.Models.Notifications
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int ManagerId { get; set; }
    }
}
