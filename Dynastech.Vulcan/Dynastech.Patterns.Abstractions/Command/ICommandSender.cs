using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public interface ICommandSender
    {
        Task SendAsync(ICommand command, CancellationToken cancellationToken = default);
    }
}
