using System.Text.Json;
using System.Text;
using Polly;
using RouteeFailoverMessages.Data;
using RouteeFailoverMessages.Domain.Models;

namespace RouteeFailoverMessages.Library.Services
{
    public class RouteeService : IRouteeService
    {
        private readonly RouteeDbContext _dbContext;

        public RouteeService(RouteeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Failover_Message_Response> SendFailoverMessage(Failover_Message_Request request, RouteeAuth routeeAuth)
        {
            Failover_Message_Response failover_Message_Response = null;

            try
            {
                _dbContext.Requests.Add(request);
                await _dbContext.SaveChangesAsync();

                string access_token = await Helper.Helper.GetAccessToken(routeeAuth.AppID, routeeAuth.AppSecret);
                var client = new HttpClient();

                var httpRetryPolicy = Policy
                    .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode) // Retry ONLY on failure
                    .Or<HttpRequestException>()
                    .WaitAndRetryAsync(
                        retryCount: 3,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) // 2, 4, 8 seconds delay
                    );

                string url = "https://connect.routee.net/failover";
                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {access_token}");

                string jsonBody = JsonSerializer.Serialize(request, new JsonSerializerOptions { WriteIndented = true });
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Executes the request with retry logic on failure
                var response = await httpRetryPolicy.ExecuteAsync(() => client.PostAsync(url, content));

                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    failover_Message_Response = JsonSerializer.Deserialize<Failover_Message_Response>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    _dbContext.Responses.Add(failover_Message_Response);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine($"API call failed: {response.StatusCode} - {responseContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return failover_Message_Response;
        }
    }
}
