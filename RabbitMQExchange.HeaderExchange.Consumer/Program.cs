﻿using RabbitMQExchange.HeaderExchangeExchange.Consumer.Service;
using System;

namespace RabbitMQExchange.HeaderExchange.Consumer
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
