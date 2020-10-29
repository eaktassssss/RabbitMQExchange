using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQExchange.TopicExchangeExchange.Publisher.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQExchange.TopicExchange.Consumer.Service
{
    public class ConsumerService
    {
        public void Consumer()
        {
            try
            {
                var factory = new ConnectionFactory();
                factory.HostName = "localhost";
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "system-logs", durable: true, type: ExchangeType.Topic);
                        string routingKey = string.Empty;
                        var queueName = channel.QueueDeclare().QueueName;
                        //routingKey = $"Error.*.Warning"; Eror ile  başlasın.Farketmez.Warning ile bitenleri dinle
                        //routingKey = $"#.Warning"; Sonu Warning ile bitenleri.
                        routingKey = $"#";// hepsini dinle
                        channel.QueueBind(queue: queueName, exchange: "system-logs", routingKey: routingKey);
                        channel.BasicQos(0, 1, false);
                        var consumer = new EventingBasicConsumer(channel);
                        channel.BasicConsume(queueName, false, consumer: consumer);
                        consumer.Received += (render, argument) =>
                        {
                            string message = Encoding.UTF8.GetString(argument.Body.ToArray());
                            Console.WriteLine(message);
                            channel.BasicAck(deliveryTag: argument.DeliveryTag, false);

                        };
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
