using Newtonsoft.Json;
using RabbitMQ.Client;
using Server.Repositories.Interfaces;
using System.Text;

namespace Server.Repositories.Services
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message, string apiType, string action)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "ServerAPI", durable: false, exclusive: false, autoDelete: false, arguments: null);

                // Thêm trường "Action" vào đối tượng message
                var messageWithAction = new
                {
                    Action = action,
                    ApiType = apiType,
                    Data = message
                };

                var json = JsonConvert.SerializeObject(messageWithAction);

                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: "ServerAPI", basicProperties: null, body: body);

                Console.WriteLine($" [x] Sent {message}");
            }
        }

    }
}
