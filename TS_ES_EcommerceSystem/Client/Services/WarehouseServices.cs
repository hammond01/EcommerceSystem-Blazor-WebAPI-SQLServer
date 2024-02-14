using Client.Helpers;
using Models.WarehouseModel;
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
    }
}
