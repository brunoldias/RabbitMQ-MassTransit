using MassTransit;
using RabbitMQ_MassTransit.Model;
using RabbitMQ_MassTransit.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MassTransit.Consumer
{
    public class DeliveryKeyConsumer : IConsumer<ItemKey>
    {
        public async Task Consume(ConsumeContext<ItemKey> context)
        {
            if (context.Message == null) throw new ArgumentNullException("context cannot be null");

            try
            {
                await EmailService.Send(context.Message.Email, "1234564698");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
