using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public interface IQueryProcessor
    {
        Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
    }

}