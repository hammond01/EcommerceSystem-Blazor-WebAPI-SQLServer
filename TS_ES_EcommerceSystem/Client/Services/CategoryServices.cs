using Client.Helpers;
using Models;
using System.Text.Json;

namespace Client.Services
{
    public class CategoryServices
    {
        public async Task<List<Categories>> GetCategories()
        {
            var request = await Program.httpClient_server.GetAsync($"v1/Category/gets");

            if (request.IsSuccessStatusCode)
            {
                var jsonString = await request.Content.ReadAsStringAsync();
                var json = JsonDocument.Parse(jsonString).RootElement;

                if (json.GetProperty("status").GetInt16() == 200)
                {
                    var r = json.GetProperty("data").GetObject<List<Categories>>();

                    return r;
                }
            }

            return new List<Categories>();
        }
    }
}
