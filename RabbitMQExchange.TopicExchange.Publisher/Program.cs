using RabbitMQExchange.TopicExchange.Publisher.Service;
using System;

namespace RabbitMQExchange.TopicExchange.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var publisherService = new PublisherService();
            publisherService.Publisher();
            Console.ReadLine();
        }
    }
}
