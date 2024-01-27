using Client.Helpers;
using Models;
using System.Text.Json;

namespace Client.Services
{
    public class SuppliersServices
    {
        private readonly HttpClient _client;
        public SuppliersServices(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<List<Suppliers>> GetSuppliers()
        {
            var request = await _client.GetAsync($"v1/Suppliers/gets");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<Suppliers>>();

                    return r;
                }
            }

            return new List<Suppliers>();
        }
    }
}
