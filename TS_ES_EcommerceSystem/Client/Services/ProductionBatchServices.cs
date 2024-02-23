using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using Client.Helpers;
using Models.WarehouseModel;
using Models.ResponseModel;

namespace Client.Services
{
    public class ProductionBatchServices
    {
        public async Task<List<ProductBathResponse>> GetProductionBatchs()
        {
            var request = await Program.httpClient.GetAsync($"ProductionBatchs/gets");
            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<ProductBathResponse>>();

                    return r;
                }
            }
            return new List<ProductBathResponse>();
        }
        public async Task<bool> CreateProductionBatch(ProductionBatch productionBatch)
        {
            var content = new StringContent(JsonConvert.SerializeObject(productionBatch), Encoding.UTF8, "application/json");
            var request = await Program.httpClient.PostAsync("ProductionBatchs/Add", content);
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
        public async Task<bool> DeleteProductionBatch(int id)
        {
            var request = await Program.httpClient.DeleteAsync($"ProductionBatchs/Delete/{id}");
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
        public async Task<ProductBathResponse> GetProductionBatchById(int id)
        {
            var request = await Program.httpClient.GetAsync($"ProductionBatchs/get/{id}");
            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;
                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data")[0].GetObject<ProductBathResponse>();
                    return r;
                }
            }
            return new ProductBathResponse();
        }
        public async Task<bool> UpdateProductionBatch(ProductBathResponse updatedProductionBatch)
        {
            var content = new StringContent(JsonConvert.SerializeObject(updatedProductionBatch), Encoding.UTF8, "application/json");
            var request = await Program.httpClient.PutAsync($"ProductionBatchs/update/{updatedProductionBatch.ProductionBatchID}", content);
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
    }
}
