using RabbitMQExchange.TopicExchange.Consumer.Service;
using System;

namespace RabbitMQExchange.TopicExchange.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumerService = new ConsumerService();
            consumerService.Consumer();
            Console.ReadLine();
        }
    }
}
