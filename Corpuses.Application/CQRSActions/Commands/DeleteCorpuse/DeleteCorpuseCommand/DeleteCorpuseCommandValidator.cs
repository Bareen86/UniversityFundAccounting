using Corpuses.Application.Validation;
using Corpuses.Domain.Repositories;

namespace Corpuses.Application.CQRSActions.Commands.DeleteCorpuse.DeleteCorpuseCommand
{
    public class DeleteCorpuseCommandValidator : IAsyncValidator<DeleteCorpuseCommand>
    {
        private readonly ICorpuseRepository _corpuseRepository;

        public DeleteCorpuseCommandValidator( ICorpuseRepository corpuseRepository )
        {
            _corpuseRepository = corpuseRepository;
        }

        public async Task<ValidationResult> ValidationAsync( DeleteCorpuseCommand command )
        {
            if ( await _corpuseRepository.GetByIdAsync( command.Id) == null )
            {
                return ValidationResult.Fail("Корпуса с таким id нет");
            }

            return ValidationResult.Ok();
        }
    }
}
