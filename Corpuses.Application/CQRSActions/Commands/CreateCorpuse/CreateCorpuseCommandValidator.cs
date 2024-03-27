using Corpuses.Application.Validation;
using Corpuses.Domain.Repositories;

namespace Corpuses.Application.CQRSActions.Commands.CreateCorpuse
{
    public class CreateCorpuseCommandValidator : IAsyncValidator<CreateCorpuseCommand>
    {
        private readonly ICorpuseRepository _corpuseRepository;

        public CreateCorpuseCommandValidator( ICorpuseRepository corpuseRepository )
        {
            _corpuseRepository = corpuseRepository;
        }

        public async Task<ValidationResult> ValidationAsync( CreateCorpuseCommand command )
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

            if ( await _corpuseRepository.GetByNameAndAddressAsync( command.Name, command.Address ) != null )
            {
                return ValidationResult.Fail( "Такой корпус уже есть на указанном адресе" );
            }
            return ValidationResult.Ok();
        }
    }
}
