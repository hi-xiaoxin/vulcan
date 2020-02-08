using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
