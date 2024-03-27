using Corpuses.Application.Validation;
using Corpuses.Domain.Repositories;

namespace Corpuses.Application.CQRSActions.Commands.UpdateCorpuse
{
    public class UpdateCorpuseCommandValidator : IAsyncValidator<UpdateCorpuseCommand>
    {
        private readonly ICorpuseRepository _corpuseRepository;

        public UpdateCorpuseCommandValidator(ICorpuseRepository corpuseRepository)
        {
            _corpuseRepository = corpuseRepository;
        }

        public async Task<ValidationResult> ValidationAsync( UpdateCorpuseCommand command )
        {
            if ( await _corpuseRepository.GetByIdAsync( command.Id ) == null )
            {
                return ValidationResult.Fail( "Корпуса с таким id нет" );
            }
            return ValidationResult.Ok();
        }
    }
}
