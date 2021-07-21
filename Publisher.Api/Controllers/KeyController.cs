using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Publisher.Api.Configuration;
using RabbitMQ_MassTransit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyController : ControllerBase
    {
        public readonly IBusControl _bus;
        public readonly IConfiguration _configuration;
        public readonly RabbitMQconfig _config;

        public KeyController(IConfiguration configuration)
        {
            _configuration = configuration;
            _config = _configuration.GetSection("RabbitMQConfig").Get<RabbitMQconfig>();

            _bus = Bus.Factory.CreateUsingRabbitMq(c =>
            {
                c.Host(new Uri(_config.Address), h =>
                {
                    h.Username(_config.Username);
                    h.Password(_config.Password);
                });
            });
        }

        [HttpPost]
        public ActionResult Post([FromBody] ItemKey itemKey)
        {
            try
            {
                if (itemKey == null)
                    throw new ArgumentNullException("Item cannot be null.");

                if (itemKey.ProductId == 0)
                    throw new ArgumentNullException("Product cannot be null.");

                if (string.IsNullOrEmpty(itemKey.Reference))
                    throw new ArgumentNullException("Reference cannot be empty.");

                _bus.Start();

                var endPoint = _bus.GetSendEndpoint(new Uri(string.Concat(_config.Address, "/", _config.QueueName)));

                if (endPoint == null)
                    throw new ArgumentNullException($"Error while try to access endpoint.");

                endPoint.Result.Send<ItemKey>(new
                {
                    Reference = itemKey.Reference,
                    ProductId = itemKey.ProductId,
                    Email = itemKey.Email
                });

                _bus.Stop();

                return Ok(new { Message = "Successufuly", Error = false });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"occurred an error, please contact support", Error = true });
            }
        }
    }
}
