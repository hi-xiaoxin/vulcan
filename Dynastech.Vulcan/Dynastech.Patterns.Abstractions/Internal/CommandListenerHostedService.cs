using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public class CommandListenerHostedService : ICommandListenerService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICommandListener _commandListener;
        private readonly ILogger _logger;

        public CommandListenerHostedService(
            IServiceProvider serviceProvider,
            ICommandListener commandListener,
            ILogger<CommandListenerHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _commandListener = commandListener;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _commandListener.Received += CommandReceive;
            _commandListener.StartListen();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _commandListener.Close();
            _commandListener.Received -= CommandReceive;
            return Task.CompletedTask;
        }

        private void CommandReceive(object sender, CommandReceivedEventArgs e)
        {
            using var serviceScope = _serviceProvider.CreateScope();
            serviceScope.ServiceProvider
              .GetRequiredService<ICommandProcessor>()
              .ProcessAsync(e.Command)
              .Wait();
        }
    }
}