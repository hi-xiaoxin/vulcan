using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public class EventPublisher : IEventPublisher
    {
        public virtual Task PublishAsync(string routingKey, IEvent @event, CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}
