using RabbitMQExchange.HeaderExchange.Publisher.Service;
using System;

namespace RabbitMQExchange.HeaderExchange.Publisher
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
