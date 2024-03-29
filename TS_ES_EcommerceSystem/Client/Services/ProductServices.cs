﻿using Newtonsoft.Json;
using Models;
using Client.Helpers;
using System.Text;
using System.Text.Json;

namespace Client.Services
{
    public class ProductServices
    {
        public async Task<(List<Products>, int)> GetProducts(int page, int pageSize, string productName)
        {
            var queryString = $"?page={page}&pageSize={pageSize}&productName={productName}";
            var request = await Program.httpClient_server.GetAsync($"v1/Products/get-product-continued{queryString}");

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
        public async Task<(List<Products>, int)> GetProductsDisContinued(int page, int pageSize, string productName)
        {
            var queryString = $"?page={page}&pageSize={pageSize}&productName={productName}";
            var request = await Program.httpClient_server.GetAsync($"v1/Products/get-product-discontinued{queryString}");

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
        public async Task<List<Products>> GetProductsInProductionBatch()
        {
            var request = await Program.httpClient_server.GetAsync($"v1/Products/get-all");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<Products>>();

                    return r;
                }
            }

            return new List<Products>();
        }
        public async Task<string> CreateProduct(Products product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            var request = await Program.httpClient_server.PostAsync("v1/Products/Add", content);

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
        public async Task<bool> DeleteProduct(int productId, bool reIntroduce)
        {
            // Assuming you have an API endpoint for deleting a product
            var request = await Program.httpClient_server.DeleteAsync($"v1/Products/Delete/{productId}?reIntroduce={reIntroduce}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    // Product deleted successfully
                    return true;
                }
            }

            // Handle deletion failure or other scenarios
            return false;
        }

        public async Task<Products> GetProductById(int productId)
        {
            // Assuming you have an API endpoint for getting a product by ID
            var request = await Program.httpClient_server.GetAsync($"v1/Products/get-product/{productId}");

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
            var request = await Program.httpClient_server.PutAsync($"v1/Products/update/{updatedProduct.ProductID}", content);

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
