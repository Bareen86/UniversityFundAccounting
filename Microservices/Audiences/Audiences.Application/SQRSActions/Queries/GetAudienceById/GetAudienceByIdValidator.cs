using Audiences.Application.SQRSActions.Queries.GetAudienceByCorpuseId;
using Audiences.Application.Validation;
using Audiences.Domain.Repositories;

namespace Audiences.Application.SQRSActions.Queries.GetAudienceById
{
    public class GetAudienceByIdValidator : IAsyncValidator<GetAudienceByIdQuery>
    {
        private readonly IAudienceRepository _audienceRepository;

        public GetAudienceByIdValidator(IAudienceRepository audienceRepository)
        {
            _audienceRepository = audienceRepository;
        }

        public async Task<ValidationResult> ValidationAsync( GetAudienceByIdQuery query )
        {
            if ( await _audienceRepository.GetAudienceByIdAsync( query .Id) == null )
            {
                return ValidationResult.Fail("Такой аудитории нет");
            }

            return ValidationResult.Ok();
        }
    }
}
