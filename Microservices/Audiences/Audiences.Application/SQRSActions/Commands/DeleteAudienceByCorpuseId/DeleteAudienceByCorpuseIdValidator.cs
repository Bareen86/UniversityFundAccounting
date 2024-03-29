using Audiences.Application.Validation;
using Audiences.Domain.Repositories;

namespace Audiences.Application.SQRSActions.Commands.DeleteAudienceByCorpuseId
{
    public class DeleteAudienceByCorpuseIdValidator : IAsyncValidator<DeleteAudienceByCorpuseIdCommand>
    {
        private readonly IAudienceRepository _audienceRepository;

        public DeleteAudienceByCorpuseIdValidator( IAudienceRepository audienceRepository)
        {
            _audienceRepository = audienceRepository;
        }

        public async Task<ValidationResult> ValidationAsync( DeleteAudienceByCorpuseIdCommand command )
        {
            if ( await _audienceRepository.GetAudiencesByCorpuseIdAsync( command.Id) == null )
            {
                return ValidationResult.Fail("Аудиторий в данном корпусе нет");
            }

            return ValidationResult.Ok();
        }
    }
}
