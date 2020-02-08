using Dynastech.Patterns;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class QueryableExtensions
    {
        public static async Task<PagingQueryResult<T>> PagingAsync<T>(this IQueryable<T> source, PagingQuery<T> query)
        {
            var queryable = source;

            if (query.SortProperty != null)
            {
                var parameterExpression = Expression.Parameter(typeof(T));
                var propertyExpression = Expression.Property(parameterExpression, query.SortProperty);
                var sortExpression = Expression.Lambda(propertyExpression, parameterExpression);

                queryable = (IQueryable<T>)queryable.Provider.CreateQuery(Expression.Call(typeof(Queryable),
                                query.SortDirection == SortDirection.Ascending ? nameof(Queryable.OrderBy) : nameof(Queryable.OrderByDescending),
                                new[] { source.ElementType, sortExpression.Body.Type },
                                source.Expression,
                                Expression.Quote(sortExpression)));
            }

            var pageIndex = query.PageIndex;
            var pageSize = query.PageSize;
            if (pageIndex < 0)
                pageIndex = 0;

            if (pageSize > 0)
            {
                var count = await queryable.CountAsync();
                var items = await queryable.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

                return new PagingQueryResult<T>
                {
                    TotalCount = count,
                    Items = items,
                    HasNext = count > (pageIndex * pageSize + pageSize)
                };
            }
            else
            {
                var items = await queryable.ToListAsync();
                return new PagingQueryResult<T>
                {
                    TotalCount = items.Count,
                    Items = items,
                    HasNext = false
                };
            }
        }
    }
}
