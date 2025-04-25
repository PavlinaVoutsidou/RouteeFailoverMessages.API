using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RouteeFailoverMessages.Domain.Models;
using RouteeFailoverMessages.Library.Services;

namespace RouteeFailoverMessages.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteeControler : ControllerBase
    {
        private readonly RouteeAuth _routeeAuth;
        private readonly IRouteeService _routeeService;

        public RouteeControler(IOptions<RouteeAuth> routeeAuth, IRouteeService routeeService)
        {
            _routeeAuth = routeeAuth.Value; // Get the configured values
            _routeeService = routeeService;

        }

        [HttpPost("send")]
        public async Task<IActionResult> GetSend([FromBody] Failover_Message_Request request)
        {
            Failover_Message_Response response =  await _routeeService.SendFailoverMessage(request,_routeeAuth);

            return Ok(response);
        }
    }
}
