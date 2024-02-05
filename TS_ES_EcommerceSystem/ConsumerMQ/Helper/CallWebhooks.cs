using Newtonsoft.Json;
using System;
using System.Text.Json;
using System.Text;
namespace ConsumerMQ.Helper
{
    public class CallWebhooks
    {
        public async Task<bool> Create<T>(T jsonData, string action, string apiType_searcch)
        {
            const string url_Elasticsearch = "https://localhost:7249/api";
            const string url_Search = "https://localhost:7249/api";
            string _url = "";
            if (apiType_searcch == "Elasticsearchs")
                _url = url_Elasticsearch;
            if (apiType_searcch == "demo")
                _url = url_Search;

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");

                var request = await client.PostAsync($"{_url}/{apiType_searcch}/{action}", content);

                if (request.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

    }
}
