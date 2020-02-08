using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReadOnlyList<MessageHandlerRouter> _routes;

        public CommandProcessor(IServiceProvider serviceProvider, IReadOnlyList<MessageHandlerRouter> routes)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _routes = routes ?? throw new ArgumentNullException(nameof(routes));
        }

        public Task ProcessAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var commandType = command.GetType();
            var router = _routes.FirstOrDefault(x => x.MessageType == commandType);
            if (router == null)
                throw new InvalidOperationException($"没有注册命令的处理程序,Type={commandType.Name}");

            var handler = this._serviceProvider.GetRequiredService(router.HandlerType);
            return (Task)router.MethodInfo.Invoke(handler, new object[] { command, cancellationToken });
        }
    }
}
