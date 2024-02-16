using Client.Helpers;
using Models.ResponseModel;
using Models.WarehouseModel;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Client.Services
{
    public class StockInBoundServices
    {
        public async Task<List<StockInBoundResponse>> GetStockInbounds()
        {
            var request = await Program.httpClient.GetAsync($"StockInbounds/gets");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<StockInBoundResponse>>();

                    return r;
                }
            }

            return new List<StockInBoundResponse>();
        }
        public async Task<string> CreateStockInBound(StockInbound stockInbound)
        {
            var content = new StringContent(JsonConvert.SerializeObject(stockInbound), Encoding.UTF8, "application/json");

            var request = await Program.httpClient.PostAsync("StockInbounds/Add", content);

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    return "Created";
                }
            }
            return "";
        }
        public async Task<string> DeleteStockInbound(int id)
        {
            // Assuming you have an API endpoint for deleting a product
            var request = await Program.httpClient.DeleteAsync($"StockInbounds/Delete/{id}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    // StockInbound deleted successfully
                    return "Deleted";
                }
            }

            // Handle deletion failure or other scenarios
            return "Deletion failed";
        }
        public async Task<bool> UpdateStockInbound(StockInBoundResponse updatedStockInbound)
        {
            var content = new StringContent(JsonConvert.SerializeObject(updatedStockInbound), Encoding.UTF8, "application/json");

            // Assuming you have an API endpoint for updating a product
            var request = await Program.httpClient.PutAsync($"StockInbounds/update/{updatedStockInbound.InboundID}", content);

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    return true;
                }
            }

            // Handle update failure or other scenarios
            return false;
        }
        public async Task<StockInBoundResponse> GetStockInbound(int id)
        {
            // Assuming you have an API endpoint for getting a product by ID
            var request = await Program.httpClient.GetAsync($"StockInbounds/get/{id}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data")[0].GetObject<StockInBoundResponse>();
                    return r;
                }
            }

            // Handle failure or other scenarios
            return new StockInBoundResponse();
        }
    }
}
