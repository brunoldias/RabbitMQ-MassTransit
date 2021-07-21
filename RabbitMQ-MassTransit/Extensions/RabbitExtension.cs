using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ_MassTransit.Configuration;
using RabbitMQ_MassTransit.Consumer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ_MassTransit
{
    public  static class RabbitExtension
    {
        public static IServiceCollection AddRabbitService(this IServiceCollection service, IConfiguration configuration)
        {
            var rabbitConfig = configuration.GetSection("RabbitMQConfig").Get<RabbitMqConfig>();

            service.AddMassTransit(options =>
            {
                options.AddConsumersFromNamespaceContaining<Worker>();

                options.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(rabbitConfig.Address), h =>
                    {
                        h.Username(rabbitConfig.Username);
                        h.Password(rabbitConfig.Password);
                    });

                    cfg.ReceiveEndpoint(rabbitConfig.QueueName, e =>
                    {
                        e.ConfigureConsumer<DeliveryKeyConsumer>(context);
                    });
                });

            });

            return service;
        }
    }
}
