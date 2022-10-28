using Azure.Messaging.ServiceBus;
using Dominio.Models;
using Infraestrutura.Interface;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infraestrutura.Servicos
{
    public class EstadoQueueConsumer : IEstadoServiceBusConsumer
    {
        private readonly ILogger _logger;
        private readonly IEstadoRepositorio _estadoRepositorio;
        private readonly ServiceBusClient _serviceBusClient;
        private ServiceBusProcessor _serviceBusProcessor;
        public EstadoQueueConsumer(IEstadoRepositorio estadoRepositorio, ServiceBusClient serviceBusClient, ILogger<EstadoQueueConsumer> logger)
        {
            _estadoRepositorio = estadoRepositorio;
            this._serviceBusClient = serviceBusClient;
            _logger = logger;
        }

        public async Task RegisterOnMessageHandlerAndReceiveMessages()
        {
            ServiceBusReceiver receiver = _serviceBusClient.CreateReceiver("estado");
            var messageHandlerOptions = new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = false
            };

            _serviceBusProcessor = _serviceBusClient.CreateProcessor("estado", messageHandlerOptions);
            _serviceBusProcessor.ProcessMessageAsync += ProcessMessagesAsync;
            _serviceBusProcessor.ProcessErrorAsync += ProcessErrorAsync;
            await _serviceBusProcessor.StartProcessingAsync().ConfigureAwait(false);
        }

        private async Task ProcessMessagesAsync(ProcessMessageEventArgs args)
        {
            Estado _estado = args.Message.Body.ToObjectFromJson<Estado>();

            await _estadoRepositorio.InsertEstado(_estado);
            await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            _logger.LogError(arg.Exception, "Message handler encountered an exception");
            _logger.LogDebug($"- ErrorSource: {arg.ErrorSource}");
            _logger.LogDebug($"- Entity Path: {arg.EntityPath}");
            _logger.LogDebug($"- FullyQualifiedNamespace: {arg.FullyQualifiedNamespace}");

            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (_serviceBusProcessor != null)
            {
                await _serviceBusProcessor.DisposeAsync().ConfigureAwait(false);
            }

            if (_serviceBusClient != null)
            {
                await _serviceBusClient.DisposeAsync().ConfigureAwait(false);
            }
        }

        public async Task CloseQueueAsync()
        {
            await _serviceBusProcessor.CloseAsync().ConfigureAwait(false);
        }
    }
}
