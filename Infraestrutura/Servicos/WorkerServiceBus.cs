using Infraestrutura.Interface;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infraestrutura.Servicos
{
    public class WorkerServiceBus : IHostedService, IDisposable
    {
        private readonly ILogger<WorkerServiceBus> _logger;
        private readonly IEstadoServiceBusConsumer _serviceBusConsumer;
        private readonly ICidadeServiceBusConsumer _cidadeServiceBusConsumer;
        private readonly IPrevisaoClimaServiceBusConsumer _previsaoClimaServiceBusConsumer;


        public WorkerServiceBus(IEstadoServiceBusConsumer serviceBusConsumer, ILogger<WorkerServiceBus> logger, ICidadeServiceBusConsumer cidadeServiceBusConsumer, IPrevisaoClimaServiceBusConsumer previsaoTempoServiceBusConsumer)
        {
            _logger = logger;
            _serviceBusConsumer = serviceBusConsumer;
            _cidadeServiceBusConsumer = cidadeServiceBusConsumer;
            _previsaoClimaServiceBusConsumer = previsaoTempoServiceBusConsumer;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Starting the service bus queue consumer and the subscription");
            await _serviceBusConsumer.RegisterOnMessageHandlerAndReceiveMessages().ConfigureAwait(false);
            await _cidadeServiceBusConsumer.RegisterOnMessageHandlerAndReceiveMessages().ConfigureAwait(false);
            await _cidadeServiceBusConsumer.RegisterOnMessageHandlerAndReceiveMessages().ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Stopping the service bus queue consumer and the subscription");
            await _serviceBusConsumer.CloseQueueAsync().ConfigureAwait(false);
            await _cidadeServiceBusConsumer.CloseQueueAsync().ConfigureAwait(false);
            await _previsaoClimaServiceBusConsumer.CloseQueueAsync().ConfigureAwait(false);
         
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async void Dispose(bool disposing)
        {
            if (disposing)
            {
                await _serviceBusConsumer.DisposeAsync().ConfigureAwait(false);
                await _cidadeServiceBusConsumer.DisposeAsync().ConfigureAwait(false);
                await _previsaoClimaServiceBusConsumer.DisposeAsync().ConfigureAwait(false);
            }
        }
    }
}
