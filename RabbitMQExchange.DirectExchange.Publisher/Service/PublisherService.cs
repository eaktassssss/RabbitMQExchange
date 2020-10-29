using RabbitMQ.Client;
using RabbitMQExchange.DirectExchange.Publisher.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQExchange.DirectExchange.Publisher.Service
{
    public class PublisherService
    {
        public void Publisher()
        {
            try
            {
                var logNames = Enum.GetValues(typeof(LogTypes));
                var factory = new ConnectionFactory();
                factory.HostName = "localhost";
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "system-logs", durable: true, type: ExchangeType.Direct);
                        var propertis = channel.CreateBasicProperties();
                        propertis.Persistent = true;
                        for (int i = 0; i < 11; i++)
                        {
                            Random random = new Random();
                            LogTypes logType = (LogTypes)logNames.GetValue(random.Next(logNames.Length));
                            var body = Encoding.UTF8.GetBytes($"log={logType.ToString()}");
                            channel.BasicPublish("system-logs", routingKey: logType.ToString(), propertis, body: body);
                        }
                        Console.WriteLine("Mesaj Direct Exchange'e gönderilmiştir");
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
