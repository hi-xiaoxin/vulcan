using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public class EventListenerHostedService : IEventListenerService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventListener _eventListener;
        private readonly ILogger _logger;

        public EventListenerHostedService(
            IServiceProvider serviceProvider,
            IEventListener eventListener,
            ILogger<EventListenerHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _eventListener = eventListener;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _eventListener.Received += MessageReceive;
            _eventListener.StartListen();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _eventListener.Close();
            _eventListener.Received -= MessageReceive;
            return Task.CompletedTask;
        }

        private void MessageReceive(object sender, EventReceivedEventArgs e)
        {
            if (e.Event == null)
                _logger.LogWarning($"事件监听时收到空命令, RoutingKey:{e.RoutingKey}");

            using var serviceScope = _serviceProvider.CreateScope();
            serviceScope.ServiceProvider
                .GetRequiredService<IEventProcessor>()
                .ProcessAsync(e.Event)
                .Wait();
        }
    }
}
