using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ_MassTransit.Model
{
    public class ItemKey
    {
        public string Reference { get; set; }
        public int ProductId { get; set; }
        public string Email { get; set; }
    }
}
