using Audiences.Application.Results;

namespace Audiences.Application.SQRSInterfaces
{
    public interface IQueryHandler<TResult, TQuery>
        where TResult : class
        where TQuery : class
    {
        Task<QueryResult<TResult>> HandleAsync( TQuery query );
    }
}
