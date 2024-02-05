using ElasticSearchModelBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace WebHooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHooksController : ControllerBase
    {
        private readonly HttpClient _client;
        public WebHooksController(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        [HttpPost("webhook/addproduct")]
        public async Task<IActionResult> createProduct(EProduct eProduct)
        {
            var content = new StringContent(JsonConvert.SerializeObject(eProduct), Encoding.UTF8, "application/json");

            var request = await _client.PostAsync("v1/Products/Add", content);

            return Ok(request);
        }
    }
}
