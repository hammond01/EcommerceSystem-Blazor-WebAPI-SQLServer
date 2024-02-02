using Newtonsoft.Json;
using System.Text;

namespace Server.Helper
{
    public class WebHookConfig
    {
        private readonly ILogger<WebHookConfig> _logger;

        public WebHookConfig(ILogger<WebHookConfig> logger)
        {
            _logger = logger;
        }

        public void NotifyWebhookApi(object data, string eventType)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var webhookApiEndpoint = $"http://localhost:7032/api/webhooks/{eventType}"; // Cập nhật đúng endpoint của API Webhook
                    var jsonPayload = JsonConvert.SerializeObject(data);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    var response = httpClient.PostAsync(webhookApiEndpoint, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation($"Webhook notification ({eventType}) sent successfully.");
                    }
                    else
                    {
                        _logger.LogError($"Failed to send webhook notification ({eventType}). Status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending webhook notification: {ex.Message}");
            }
        }
    }


}
