﻿using RabbitMQ.Client;
using RabbitMQExchange.DirectExchange.Publisher.Enums;
using RabbitMQExchange.DirectExchange.Publisher.Service;
using System;
using System.Text;

namespace RabbitMQExchange.DirectExchange.Publisher
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
