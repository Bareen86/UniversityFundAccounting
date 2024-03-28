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
            if ( command.Name == null || command.Name == String.Empty )
            {
                return ValidationResult.Fail( "Имя корпуса не должно быть пустым" );
            }

            if ( command.Address == null || command.Address == String.Empty )
            {
                return ValidationResult.Fail( "Адресс не должен быть пустым" );
            }

            if ( command.FloorsNumber <= 0 )
            {
                return ValidationResult.Fail( "В корпусе должен быть минимум 1 этаж" );
            }

            if ( await _corpuseRepository.GetByIdAsync( command.Id ) == null )
            {
                return ValidationResult.Fail( "Корпуса с таким id нет" );
            }
            return ValidationResult.Ok();
        }
    }
}
