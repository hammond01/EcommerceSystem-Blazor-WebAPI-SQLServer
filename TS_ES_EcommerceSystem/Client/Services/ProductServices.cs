using Newtonsoft.Json;
using Models;
using Client.Helpers;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Client.Services
{
    public class ProductServices
    {
        private readonly HttpClient _client;
        public ProductServices(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<(List<Products>, int)> GetProducts(int page, int pageSize, string productName)
        {
            var queryString = $"?page={page}&pageSize={pageSize}&productName={productName}";
            var request = await _client.GetAsync($"v1/Products/gets{queryString}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<Products>>();
                    var totalPage = json.GetProperty("totalPage").GetInt16();

                    return (r, totalPage);
                }
            }

            return (new List<Products>(), 0);
        }
        public async Task<string> CreateProduct(Products product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            var request = await _client.PostAsync("v1/Products/Add", content);

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    //var r = json.GetProperty("data").GetObject<Products>();
                    return "Created";
                }
            }
            return "";
        }
        public async Task<string> DeleteProduct(int productId)
        {
            // Assuming you have an API endpoint for deleting a product
            var request = await _client.DeleteAsync($"v1/Products/Delete/{productId}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    // Product deleted successfully
                    return "Deleted";
                }
            }

            // Handle deletion failure or other scenarios
            return "Deletion failed";
        }

        public async Task<Products> GetProductById(int productId)
        {
            // Assuming you have an API endpoint for getting a product by ID
            var request = await _client.GetAsync($"v1/Products/get-product/{productId}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<Products>();
                    return r;
                }
            }

            // Handle failure or other scenarios
            return new Products();
        }
        public async Task<bool> UpdateProduct(Products updatedProduct)
        {
            var content = new StringContent(JsonConvert.SerializeObject(updatedProduct), Encoding.UTF8, "application/json");

            // Assuming you have an API endpoint for updating a product
            var request = await _client.PutAsync($"v1/Products/update/{updatedProduct.ProductID}", content);

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
