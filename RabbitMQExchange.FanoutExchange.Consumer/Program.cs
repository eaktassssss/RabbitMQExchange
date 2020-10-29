using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQExchange.FanoutExchange.Consumer.Service;
using System;
using System.Text;

namespace RabbitMQExchange.FanoutExchange.Consumer
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
