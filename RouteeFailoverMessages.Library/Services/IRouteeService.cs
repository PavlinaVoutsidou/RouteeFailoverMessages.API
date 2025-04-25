using RouteeFailoverMessages.Domain.Models;

namespace RouteeFailoverMessages.Library.Services
{
    public interface IRouteeService
    {
        public Task<Failover_Message_Response> SendFailoverMessage(Failover_Message_Request request, RouteeAuth routeeAuth);
    }
}
