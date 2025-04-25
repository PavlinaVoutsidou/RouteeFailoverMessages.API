namespace RouteeFailoverMessages.Domain.Models
{
    public class Failover_Message_Request
    {
        public List<Flow> flow { get; set; } = new List<Flow>();
        public Callback callback { get; set; } = new Callback();
    }
    public class Callback
    {
        public string strategy { get; set; } = string.Empty;
        public string url { get; set; } = string.Empty;
    }
    public class Flow
    {
        public string type { get; set; } = string.Empty;
        public string from { get; set; } = string.Empty;
        public string to { get; set; } = string.Empty;
        public decimal ttl { get; set; } = 0;
        public Message message { get; set; } = new Message();
        public int order { get; set; } = 0;
        public List<string> failoverOnStatuses { get; set; } = new List<string>();
        public string senderInfoTrackingId { get; set; } = string.Empty;
        public string inboundUrl { get; set; } = string.Empty;
        public bool expireOnDelivery { get; set; } = false;
    }
    public class Message
    {
        public string body { get; set; } = string.Empty;
        public bool flash { get; set; } = false;
        public string label { get; set; } = string.Empty;
        public bool transcode { get; set; } = false;
        public string text { get; set; } = string.Empty;
        public string imageUrl { get; set; } = string.Empty;
        public Action action { get; set; } = new Action();
    }
    public class Action
    {
        public string caption { get; set; } = string.Empty;
        public string targetUrl { get; set; } = string.Empty;
    }

}