using Corpuses.Application.Validation;
using Corpuses.Domain.Repositories;

namespace Corpuses.Application.CQRSActions.Queries.GetCorpuse
{
    public class GetCorpuseQueryValidator : IAsyncValidator<GetCorpuseQuery>
    {
        private readonly ICorpuseRepository _corpuseRepository;

        public GetCorpuseQueryValidator( ICorpuseRepository corpuseRepository )
        {
            _corpuseRepository = corpuseRepository;
        }

        public async Task<ValidationResult> ValidationAsync( GetCorpuseQuery query )
        {
            if ( await _corpuseRepository.GetByIdAsync( query.Id ) == null )
            {
                return ValidationResult.Fail( "Корпуса с таким id нет" );
            }

            return ValidationResult.Ok();
        }
    }
}
