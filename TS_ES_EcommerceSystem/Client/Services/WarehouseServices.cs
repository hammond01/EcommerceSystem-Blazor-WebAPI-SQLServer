using Client.Helpers;
using Models.ResponseModel;
using Models.WarehouseModel;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Client.Services
{
    public class WarehouseServices
    {
        public async Task<List<WareHouse>> GetWareHouses()
        {
            var request = await Program.httpClient.GetAsync($"WareHouses/gets");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<WareHouse>>();

                    return r;
                }
            }

            return new List<WareHouse>();
        }
        public async Task<bool> Delete(int id)
        {
            // Assuming you have an API endpoint for deleting a product
            var request = await Program.httpClient.DeleteAsync($"WareHouses/Delete/{id}");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    // ProductionBatch deleted successfully
                    return true;
                }
            }

            // Handle deletion failure or other scenarios
            return false;
        }
        public async Task<bool> Create(WareHouse obj)
        {
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var request = await Program.httpClient.PostAsync("WareHouses/Add", content);

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
        public async Task<bool> Update(WareHouse obj)
        {
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            // Assuming you have an API endpoint for updating a product
            var request = await Program.httpClient.PutAsync($"WareHouses/update/{obj.WareHouseID}", content);

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
