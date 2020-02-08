using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public interface IEventProcessor
    {
        Task ProcessAsync(IEvent @event, CancellationToken cancellationToken = default);
    }
}
