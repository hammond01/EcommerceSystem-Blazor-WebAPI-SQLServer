using System.Text.Json;
using Models;
using Client.Helpers;
using System.Net.Http.Json;

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
        //public async Task<string> CreateProduct(Products product)
        //{
        //    var request = await _client.PostAsJsonAsync($"v1/Products/Add", product);
        //    if (request.IsSuccessStatusCode)
        //    {
        //        var jsonString = await request.Content.ReadAsStringAsync();
        //        var json = JsonDocument.Parse(jsonString).RootElement;

        //        if (json.GetProperty("status").GetInt16() == 200)
        //        {
        //            //var r = json.GetProperty("data").GetObject<Products>();
        //            return "Created";
        //        }
        //    }
        //    return "";
        //}
    }
}
