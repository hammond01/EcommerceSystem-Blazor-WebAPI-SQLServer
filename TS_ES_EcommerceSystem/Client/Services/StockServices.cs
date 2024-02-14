using Client.Helpers;
using Models.WarehouseModel;
using System.Text.Json;

namespace Client.Services
{
    public class StockServices
    {
        public async Task<List<StockInbound>> GetStockInbounds()
        {
            var request = await Program.httpClient.GetAsync($"StockInbounds/gets");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<StockInbound>>();

                    return r;
                }
            }

            return new List<StockInbound>();
        }
        public async Task<List<StockOutbound>> GetStockOutbounds()
        {
            var request = await Program.httpClient.GetAsync($"StockInbounds/gets");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<StockOutbound>>();

                    return r;
                }
            }

            return new List<StockOutbound>();
        }
    }
}
