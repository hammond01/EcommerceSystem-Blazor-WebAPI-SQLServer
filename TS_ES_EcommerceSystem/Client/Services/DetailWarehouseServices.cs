﻿using Client.Helpers;
using Models.ResponseModel;
using Models.WarehouseModel;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Client.Services
{
    public class DetailWarehouseServices
    {
        public async Task<List<WarehouseResponse>> GetWarehouseInformation(int id)
        {
            // Assuming you have an API endpoint for getting a product by ID
            var request = await Program.httpClient.GetAsync($"warehouses/get/{id}");

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

            return new List<WarehouseResponse>();
        }
        public async Task<string> CreateDataInWarehouseWhenInbound(DetailWarehouseResponse obj)
        {
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var request = await Program.httpClient.PostAsync("DetailWarehouses/Add", content);

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
        public async Task<bool> Update(DetailWarehouseResponse obj)
        {
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            var request = await Program.httpClient.PutAsync($"DetailWarehouses/update/{obj.DetailWarehouseID}", content);
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
