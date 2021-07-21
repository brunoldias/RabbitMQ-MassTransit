﻿using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ_MassTransit.Configuration;
using System;

namespace RabbitMQ_MassTransit
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureServices((hostingContext, service) =>
              {
                    var configuration = hostingContext.Configuration;

                    service.AddRabbitService(configuration);
                    service.AddHostedService<Worker>();
              });
    }
}
