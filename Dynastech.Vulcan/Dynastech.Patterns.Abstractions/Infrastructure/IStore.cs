using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public interface IStore
    {
        Task<IStoreTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task UseTransactionAsync(IStoreTransaction transaction, CancellationToken cancellationToken = default);        
    }
}
