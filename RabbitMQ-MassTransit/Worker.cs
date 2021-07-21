using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ_MassTransit
{
    public class Worker : IHostedService
    {
        private IBusControl _bus;
        public Worker(IBusControl bus)
        {
            _bus  = bus;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _bus.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _bus.StartAsync(cancellationToken);
        }
    }
}
