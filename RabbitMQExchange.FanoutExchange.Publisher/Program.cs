using RabbitMQ.Client;
using RabbitMQExchange.FanoutExchange.Publisher.Service;
using System;
using System.Text;

namespace RabbitMQExchange.FanoutExchange.Publisher
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
