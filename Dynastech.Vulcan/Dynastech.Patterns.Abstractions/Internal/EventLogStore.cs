using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public class EventLogStore : IEventLogStore
    {
        public Task<IStoreTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return null;
        }

        public virtual Task LogAsync(EventLog log) => Task.CompletedTask;

        public Task UseTransactionAsync(IStoreTransaction transaction, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
