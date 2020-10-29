﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQExchange.DirectExchange.Consumer.Enums;
using RabbitMQExchange.DirectExchange.Consumer.Service;
using System;
using System.Text;

namespace RabbitMQExchange.DirectExchange.Consumer
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
