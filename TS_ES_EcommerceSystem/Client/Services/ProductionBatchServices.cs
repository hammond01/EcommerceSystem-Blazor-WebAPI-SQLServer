using Models;
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
        public async Task<List<ResProductionBatch>> GetProductionBatchs()
        {
            var request = await Program.httpClient.GetAsync($"ProductionBatchs/gets");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<ResProductionBatch>>();

                    return r;
                }
            }

            return new List<ResProductionBatch>();
        }
        public async Task<string> CreateProductionBatch(ProductionBatch productionBatch)
        {
            var content = new StringContent(JsonConvert.SerializeObject(productionBatch), Encoding.UTF8, "application/json");

            var request = await Program.httpClient.PostAsync("ProductionBatchs/Add", content);

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    //var r = json.GetProperty("data").GetObject<ProductionBatchs>();
                    return "Created";
                }
            }
            return "";
        }
        public async Task<string> DeleteProductionBatch(int id)
        {
            // Assuming you have an API endpoint for deleting a product
            var request = await Program.httpClient.DeleteAsync($"ProductionBatchs/Delete/{id}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    // ProductionBatch deleted successfully
                    return "Deleted";
                }
            }

            // Handle deletion failure or other scenarios
            return "Deletion failed";
        }

        public async Task<ResProductionBatch> GetProductionBatchById(int id)
        {
            // Assuming you have an API endpoint for getting a product by ID
            var request = await Program.httpClient.GetAsync($"ProductionBatchs/get/{id}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data")[0].GetObject<ResProductionBatch>();
                    return r;
                }
            }

            // Handle failure or other scenarios
            return new ResProductionBatch();
        }
        public async Task<bool> UpdateProductionBatch(ResProductionBatch updatedProductionBatch)
        {
            var content = new StringContent(JsonConvert.SerializeObject(updatedProductionBatch), Encoding.UTF8, "application/json");

            // Assuming you have an API endpoint for updating a product
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

            // Handle update failure or other scenarios
            return false;
        }
    }
}
