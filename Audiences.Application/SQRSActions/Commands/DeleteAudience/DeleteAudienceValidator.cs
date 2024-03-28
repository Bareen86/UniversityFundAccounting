using Audiences.Application.Validation;
using Audiences.Domain.Repositories;

namespace Audiences.Application.SQRSActions.Commands.DeleteAudience
{
    public class DeleteAudienceValidator : IAsyncValidator<DeleteAudienceCommand>
    {
        private readonly IAudienceRepository _audienceRepository;

        public DeleteAudienceValidator( IAudienceRepository audienceRepository )
        {
            _audienceRepository = audienceRepository;
        }

        public async Task<ValidationResult> ValidationAsync( DeleteAudienceCommand command )
        {
            if ( await _audienceRepository.GetAudienceByIdAsync( command.Id ) == null )
            {
                return ValidationResult.Fail( "Такой аудитории нет" );
            }

            return ValidationResult.Ok();
        }
    }
}
