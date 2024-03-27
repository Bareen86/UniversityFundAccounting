using Corpuses.Application.Results;

namespace Corpuses.Application.CQRSInterfaces
{
    public interface IQueryHandler<TResult, TQuery>
        where TResult : class
        where TQuery : class
    {
        Task<QueryResult<TResult>> HandleAsync( TQuery query);
    }
}
