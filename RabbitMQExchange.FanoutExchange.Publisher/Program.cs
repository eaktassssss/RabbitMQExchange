using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQExchange.FanoutExchange.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = "Bu bir fanout exchange kullanım örneğidir";
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "logs", durable: true, type: ExchangeType.Fanout);
                    var propertis = channel.CreateBasicProperties();
                    propertis.Persistent = true;
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("logs", routingKey: "", propertis, body: body);
                    Console.WriteLine("Mesaj Fanout Exchange'e gönderilmiştir");
                    Console.ReadLine();
                }
            }
        }
    }
}
