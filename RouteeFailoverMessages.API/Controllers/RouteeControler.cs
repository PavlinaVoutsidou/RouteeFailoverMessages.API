using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<RouteeControler> _logger;

        public RouteeControler(IOptions<RouteeAuth> routeeAuth, IRouteeService routeeService, ILogger<RouteeControler> logger)
        {
            _routeeAuth = routeeAuth.Value; // Get the configured values
            _routeeService = routeeService;
            _logger = logger;

        }

        [HttpPost("send")]
        public async Task<IActionResult> GetSend([FromBody] Failover_Message_Request request)
        {
            _logger.LogInformation("Received send request with {FlowCount} flows, Callback strategy: {CallbackStrategy}, Callback URL: {CallbackUrl}",
                request.flow.Count,
                request.callback.strategy,
                request.callback.url);

            try
            {
                Failover_Message_Response response = await _routeeService.SendFailoverMessage(request, _routeeAuth);

                _logger.LogInformation("Message sent successfully, response: {@Response}", response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending failover message");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to send message");
            }
        }
    }
}
