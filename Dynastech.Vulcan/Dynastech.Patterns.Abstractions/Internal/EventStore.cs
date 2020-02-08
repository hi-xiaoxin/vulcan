using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public class EventStore : IEventStore
    {
        public Task<IStoreTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return null;
        }

        public Task SaveAsync(IEvent @event, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task UseTransactionAsync(IStoreTransaction transaction, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
