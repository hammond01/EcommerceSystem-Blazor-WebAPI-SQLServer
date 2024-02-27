using Client.Helpers;
using Models;
using System.Text.Json;

namespace Client.Services
{
    public class SuppliersServices
    {
        public async Task<List<Suppliers>> GetSuppliers()
        {
            var request = await Program.httpClient_server.GetAsync($"v1/Suppliers/gets");

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
