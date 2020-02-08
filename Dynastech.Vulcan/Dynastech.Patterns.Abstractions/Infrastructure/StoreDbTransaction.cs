using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public class StoreDbTransaction : IStoreTransaction
    {
        private readonly DbTransaction _transaction;

        public StoreDbTransaction(DbTransaction transaction)
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
            return _transaction;
        }
    }
}