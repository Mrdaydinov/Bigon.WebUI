namespace Bigon.WebUI.Models.Entities
{
    public class Subscriber
    {
        public string EmailAddress { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}
