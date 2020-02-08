using Dynastech.Patterns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public abstract class AbstractEntityFrameworkCoreStore<TDbContext> : IStore where TDbContext : DbContext
    {
        protected TDbContext DbContext { get; }

        public AbstractEntityFrameworkCoreStore(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IStoreTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
            return new StoreDbContextTransaction(transaction);
        }

        public Task UseTransactionAsync(IStoreTransaction transaction, CancellationToken cancellationToken = default)
        {
            if (transaction == null)
                return DbContext.Database.UseTransactionAsync(null, cancellationToken);
            else
                return DbContext.Database.UseTransactionAsync(transaction.GetDbTransaction(), cancellationToken);
        }
    }
}