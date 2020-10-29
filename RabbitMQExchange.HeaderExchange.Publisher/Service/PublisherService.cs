using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQExchange.HeaderExchange.Publisher.Service
{
    public class PublisherService
    {
        public void Publisher()
        {
            try
            {
                string message = "Header Exchange kullanım örneği";
                var factory = new ConnectionFactory();
                factory.HostName = "localhost";
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "header-exchange", durable: true, type: ExchangeType.Headers);
                        var propertis = channel.CreateBasicProperties();
                        var header = new Dictionary<string, object>();
                        header.Add("Material", "Type"); 
                        propertis.Headers = header;
                        propertis.Persistent = true;
                        var body = Encoding.UTF8.GetBytes($"Message={message}");
                        channel.BasicPublish("header-exchange", routingKey: "", propertis, body: body);
                        Console.WriteLine($"Mesaj Header Exchange'e gönderilmiştir:{message}");
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
