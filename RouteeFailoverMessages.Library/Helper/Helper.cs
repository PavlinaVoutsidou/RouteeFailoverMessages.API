using System.Text;
using System.Text.Json;

namespace RouteeFailoverMessages.Library.Helper
{
    public class Helper
    {       
        public static async Task<string> GetAccessToken(string username, string password) 
        {
            string accessToken = string.Empty;

            try
            {
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://auth.routee.net/oauth/token");
                string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                request.Headers.Add("Authorization", $"Basic {credentials}");
                request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.SendAsync(request);
                string responseContent = await response.Content.ReadAsStringAsync();

                using var jsonDoc = JsonDocument.Parse(responseContent);
                accessToken = jsonDoc.RootElement.GetProperty("access_token").GetString();
            }
            catch (Exception)
            {

                throw;
            }

            return accessToken;
        }
    }
}
