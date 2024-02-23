using Client.Helpers;
using Models.ResponseModel;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Client.Services
{
    public class StockOutBoundServices
    {
        private readonly DetailWarehouseServices _detailWarehouseServices;
        public StockOutBoundServices(DetailWarehouseServices detailWarehouseServices)
        {
            _detailWarehouseServices = detailWarehouseServices;
        }
        public async Task<List<StockOutBoundResponse>> GetStockOutbounds()
        {
            var request = await Program.httpClient.GetAsync($"StockOutbounds/gets");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<StockOutBoundResponse>>();

                    return r;
                }
            }

            return new List<StockOutBoundResponse>();
        }
        public async Task<bool> CreateStockOutBound(StockOutBoundResponse stockOutbound)
        {
            var content = new StringContent(JsonConvert.SerializeObject(stockOutbound), Encoding.UTF8, "application/json");

            var request = await Program.httpClient.PostAsync("StockOutbounds/Add", content);

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<string> DeleteStockOutbound(int id)
        {
            // Assuming you have an API endpoint for deleting a product
            var request = await Program.httpClient.DeleteAsync($"StockOutbounds/Delete/{id}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    // StockOutbound deleted successfully
                    return "Deleted";
                }
            }

            // Handle deletion failure or other scenarios
            return "Deletion failed";
        }
        public async Task<bool> UpdateStockOutbound(StockOutBoundResponse updatedStockOutbound)
        {
            var content = new StringContent(JsonConvert.SerializeObject(updatedStockOutbound), Encoding.UTF8, "application/json");

            // Assuming you have an API endpoint for updating a product
            var request = await Program.httpClient.PutAsync($"StockOutbounds/update/{updatedStockOutbound.OutboundID}", content);

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
        public async Task<StockOutBoundResponse> GetStockOutbound(int id)
        {
            // Assuming you have an API endpoint for getting a product by ID
            var request = await Program.httpClient.GetAsync($"StockOutbounds/get/{id}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data")[0].GetObject<StockOutBoundResponse>();
                    return r;
                }
            }

            // Handle failure or other scenarios
            return new StockOutBoundResponse();
        }

        public async Task<List<InformationStockOutboundFromWarehouse>> GetInformationOutBoundFromWarehouseID(int id)
        {
            // Assuming you have an API endpoint for getting a product by ID
            var request = await Program.httpClient.GetAsync($"StockOutbounds/get-information-by-warehouseid/{id}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<InformationStockOutboundFromWarehouse>>();
                    return r;
                }
            }
            // Handle failure or other scenarios
            return new List<InformationStockOutboundFromWarehouse>();
        }

        public async Task<InformationStockOutboundFromWarehouse> GetInfoOutBoundByOutBoundID(int warehouseID, int OutboundID)
        {
            var response = await GetInformationOutBoundFromWarehouseID(warehouseID);
            if (response is not null)
            {
                var info = response.Where(h => h.OutboundID == OutboundID).FirstOrDefault()!;
                return info;
            }
            return new InformationStockOutboundFromWarehouse();
        }
        public async Task<WarehouseResponse> GetInfoStockOutbound(int warehouseID, int detailWarehouseID)
        {
            var response = await _detailWarehouseServices.GetWarehouseInformation(warehouseID);
            if (response is not null)
            {
                var info = response.Where(h => h.DetailWarehouseID == detailWarehouseID).FirstOrDefault()!;
                return info;
            }
            return new WarehouseResponse();
        }
        public async Task<List<WarehouseResponse>> GetInfoWarehouseActualWarehouseGreaterThanZeroByWarehouseID(int id)
        {
            // Assuming you have an API endpoint for getting a product by ID
            var request = await Program.httpClient.GetAsync($"StockOutbounds/get-information-actualwarehouse-greater-than-zero-by-warehouseid/{id}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<WarehouseResponse>>();
                    return r;
                }
            }
            // Handle failure or other scenarios
            return new List<WarehouseResponse>();
        }
    }
}
