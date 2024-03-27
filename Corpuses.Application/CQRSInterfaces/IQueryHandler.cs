using Corpuses.Application.Results;

namespace Corpuses.Application.CQRSInterfaces
{
    internal interface IQueryHandler<TResult, TQuery>
        where TResult : class
        where TQuery : class
    {
        Task<QueryResult<TResult>> HandleAsync( TQuery query);
    }
}
