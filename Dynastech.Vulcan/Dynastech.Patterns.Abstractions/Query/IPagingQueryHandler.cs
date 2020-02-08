namespace Dynastech.Patterns
{
    public interface IPagingQueryHandler<TItem> : IQueryHandler<PagingQuery<TItem>, PagingQueryResult<TItem>>
    {

    }

    public interface IPagingQueryHandler<in TQuery, TItem> : IQueryHandler<TQuery, PagingQueryResult<TItem>>
        where TQuery : PagingQuery<TItem>
    {

    }

}
