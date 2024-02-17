using Client.Helpers;
using Models.ResponseModel;
using Models.WarehouseModel;
using System.Text.Json;

namespace Client.Services
{
    public class UnitServices
    {
        public async Task<List<Units>> GetUnits()
        {
            var request = await Program.httpClient.GetAsync($"Units/gets");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;
                var r = json.GetObject<List<Units>>();
                return r;
            }


            return new List<Units>();
        }
    }
}
