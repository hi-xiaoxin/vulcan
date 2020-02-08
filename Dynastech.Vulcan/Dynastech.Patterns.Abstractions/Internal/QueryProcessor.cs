using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReadOnlyList<MessageHandlerRouter> _routes;

        public QueryProcessor(IServiceProvider serviceProvider, IReadOnlyList<MessageHandlerRouter> routes)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _routes = routes ?? throw new ArgumentNullException(nameof(routes));
        }

        public Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var queryType = query.GetType();

            var router = _routes.FirstOrDefault(x => x.MessageType == queryType);
            if (router == null)
                throw new InvalidOperationException($"没有注册查询的处理程序,Type={queryType.Name}");

            var handler = this._serviceProvider.GetRequiredService(router.HandlerType);
            return (Task<TResult>)router.MethodInfo.Invoke(handler, new object[] { query, cancellationToken });
        }
    }
}
