using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public class StoreDbContextTransaction : IStoreTransaction
    {
        private readonly IDbContextTransaction _transaction;

        public StoreDbContextTransaction(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return _transaction.CommitAsync(cancellationToken);
        }

        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            return _transaction.RollbackAsync(cancellationToken);
        }

        public ValueTask DisposeAsync()
        {
            return _transaction.DisposeAsync();
        }

        public DbTransaction GetDbTransaction()
        {
            return _transaction.GetDbTransaction();
        }
    }
}