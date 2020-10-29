using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQExchange.HeaderExchangeExchange.Consumer.Service
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
                        channel.ExchangeDeclare(exchange: "header-exchange", durable: true, type: ExchangeType.Headers);
                        var queueName = channel.QueueDeclare("HeaderExchangeQueue", true, false, false, null);
                        var header = new Dictionary<string, object>();
                        header.Add("Format", "PDF");
                        header.Add("Shape", "A4");
                        header.Add("x-match", "all");
                        channel.QueueBind(queue: queueName, exchange: "header-exchange", routingKey: "", header);
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
