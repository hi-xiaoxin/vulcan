using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public interface ICommandProcessor
    {
        Task ProcessAsync(ICommand command, CancellationToken cancellationToken = default);
    }
}
