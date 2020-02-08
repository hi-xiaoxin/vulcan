using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public interface IEventStore : IStore
    {
        Task SaveAsync(IEvent @event, CancellationToken cancellationToken = default);
    }
}