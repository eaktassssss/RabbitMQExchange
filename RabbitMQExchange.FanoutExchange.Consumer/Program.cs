using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQExchange.FanoutExchange.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "logs", durable: true,type: ExchangeType.Fanout);

                    var queueName = channel.QueueDeclare().QueueName;
                    channel.QueueBind(queue: queueName, exchange: "logs", routingKey: "");
                    channel.BasicQos(0, 1, false);
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume(queueName, false, consumer: consumer);
                    consumer.Received += (render, argument) =>
                    {
                       string message = Encoding.UTF8.GetString(argument.Body.ToArray());
                        channel.BasicAck(deliveryTag: argument.DeliveryTag, false);
                        Console.WriteLine(message);
                    };
                    Console.ReadLine();
                }
            }
        }
    }
}
