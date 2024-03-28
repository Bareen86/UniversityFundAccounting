using Audiences.Application.Validation;
using Audiences.Domain.Repositories;

namespace Audiences.Application.SQRSActions.Queries.GetAudienceByCorpuseId
{
    public class GetAudiencesByCorpuseIdQueryValidator : IAsyncValidator<GetAudiencesByCorpuseIdQuery>
    {
        private readonly IAudienceRepository _audienceRepository;

        public GetAudiencesByCorpuseIdQueryValidator( IAudienceRepository audienceRepository )
        {
            _audienceRepository = audienceRepository;
        }

        public async Task<ValidationResult> ValidationAsync( GetAudiencesByCorpuseIdQuery query )
        {
            if ( await _audienceRepository.GetAudiencesByCorpuseIdAsync( query.Id ) == null )
            {
                return ValidationResult.Fail( "Такого корпуса нет" );
            }

            return ValidationResult.Ok();
        }
    }
}
