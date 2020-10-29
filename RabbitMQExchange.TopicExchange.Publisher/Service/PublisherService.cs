using RabbitMQ.Client;
using RabbitMQExchange.TopicExchange.Publisher.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQExchange.TopicExchange.Publisher.Service
{
    public class PublisherService
    {
        public void Publisher()
        {
            try
            {
                var logNames = Enum.GetValues(typeof(LogTypes));
                var factory = new ConnectionFactory();
                var routeKey = string.Empty;
                factory.HostName = "localhost";
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "system-logs", durable: true, type: ExchangeType.Topic);
                        var propertis = channel.CreateBasicProperties();
                        propertis.Persistent = true;
                        for (int i = 0; i < 11; i++)
                        {
                            Random random = new Random();
                            LogTypes logType1 = (LogTypes)logNames.GetValue(random.Next(logNames.Length));
                            LogTypes logType2 = (LogTypes)logNames.GetValue(random.Next(logNames.Length));
                            LogTypes logType3 = (LogTypes)logNames.GetValue(random.Next(logNames.Length));
                            routeKey = $"{logType1}.{logType2}.{logType3}";
                            var body = Encoding.UTF8.GetBytes($"log={logType1.ToString()}{logType2.ToString()}{logType3.ToString()}");
                            channel.BasicPublish("system-logs", routingKey: routeKey, propertis, body: body);
                            Console.WriteLine($"Mesaj Topic Exchange'e gönderilmiştir:{routeKey}");
                        }
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
