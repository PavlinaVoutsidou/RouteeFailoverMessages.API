namespace RouteeFailoverMessages.Domain.Models
{
    public class Failover_Message_Response
    {
        public string trackingId { get; set; } = string.Empty;
        public List<Flow> flow { get; set; } = new List<Flow>();
        public Callback callback { get; set; } = new Callback();
        public DateTime createdAt { get; set; } = new DateTime();
    }
}
