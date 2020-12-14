namespace ESKOBApi.Models
{
    public class Manager : DummyManager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int TenantId { get; set; }

    }
}
