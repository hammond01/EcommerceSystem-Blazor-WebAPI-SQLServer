using ConsumerMQ.Helper;
using Models.ElasticsearchModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        const string ELASTICSEARCH = "Elasticsearchs";

        try
        {
            CallWebhooks apiHelper = new CallWebhooks();
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("ServerAPI", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var json = JsonDocument.Parse(message).RootElement;

                //send action apiType
                var apiType = json.GetProperty("ApiType").GetString();
                var actionEndpoint = json.GetProperty("Action").GetString();
                var jsonObject = json.GetProperty("Data").GetObject<EProduct>();
                #region Product action
                if (apiType!.ToLower() == "product")
                {
                    #region add product to search
                    if (actionEndpoint!.ToLower() == "add")
                    {
                        var response = await apiHelper.Create(jsonObject, actionEndpoint!, ELASTICSEARCH);
                        if (response == true)
                        {
                            Console.WriteLine("Upload product to Elasticsearch succesfull");
                            Console.WriteLine(message);
                        }
                        else
                        {
                            Console.WriteLine("Create fail");
                            Console.WriteLine(response);
                        }
                    }
                    #endregion

                    #region update product in search
                    if (actionEndpoint!.ToLower() == "update")
                    {

                    }
                    #endregion

                    #region delete product in search
                    if (actionEndpoint!.ToLower() == "delete")
                    {

                    }
                    #endregion
                }
                #endregion

            };
            channel.BasicConsume(queue: "ServerAPI", autoAck: true, consumer: consumer);
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}