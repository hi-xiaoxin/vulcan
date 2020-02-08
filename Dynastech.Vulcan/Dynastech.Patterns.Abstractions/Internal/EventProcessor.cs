using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReadOnlyList<MessageHandlerRouter> _routes;

        public EventProcessor(IServiceProvider serviceProvider, IReadOnlyList<MessageHandlerRouter> routes)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _routes = routes ?? throw new ArgumentNullException(nameof(routes));
        }

        public Task ProcessAsync(IEvent @event, CancellationToken cancellationToken = default)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            var eventType = @event.GetType();

            var router = _routes.FirstOrDefault(x => x.MessageType == eventType);
            if (router == null)
                throw new InvalidOperationException($"没有注册事件的处理程序,Type={eventType.Name}");

            var handler = this._serviceProvider.GetRequiredService(router.HandlerType);
            return (Task)router.MethodInfo.Invoke(handler, new object[] { @event, cancellationToken });
        }
    }
}