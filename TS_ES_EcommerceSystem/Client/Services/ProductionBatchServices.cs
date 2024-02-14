using Models;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using Client.Helpers;
using Models.WarehouseModel;

namespace Client.Services
{
    public class ProductionBatchionBatchServices
    {
        public async Task<List<ProductionBatch>> GetProductionBatchs()
        {
            var request = await Program.httpClient.GetAsync($"ProductionBatchionBatchs/gets");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<ProductionBatch>>();

                    return r;
                }
            }

            return new List<ProductionBatch>();
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
        public async Task<string> DeleteProductionBatch(int productId)
        {
            // Assuming you have an API endpoint for deleting a product
            var request = await Program.httpClient.DeleteAsync($"ProductionBatchs/Delete/{productId}");

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

        public async Task<ProductionBatch> GetProductionBatchById(int productId)
        {
            // Assuming you have an API endpoint for getting a product by ID
            var request = await Program.httpClient.GetAsync($"ProductionBatchs/get/{productId}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<ProductionBatch>();
                    return r;
                }
            }

            // Handle failure or other scenarios
            return new ProductionBatch();
        }
        public async Task<bool> UpdateProductionBatch(ProductionBatch updatedProductionBatch)
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
