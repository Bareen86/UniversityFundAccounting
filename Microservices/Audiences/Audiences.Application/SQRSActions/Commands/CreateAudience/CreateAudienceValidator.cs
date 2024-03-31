using Audiences.Application.Validation;
using Audiences.Domain.Repositories;
using static Audiences.Domain.AudienceTypeEnum;

namespace Audiences.Application.SQRSActions.Commands.CreateAudience
{
    public class CreateAudienceValidator : IAsyncValidator<CreateAudienceCommand>
    {
        private readonly IAudienceRepository _audienceRepository;

        public CreateAudienceValidator( IAudienceRepository audienceRepository )
        {
            _audienceRepository = audienceRepository;
        }

        public async Task<ValidationResult> ValidationAsync( CreateAudienceCommand command )
        {

            if ( await _audienceRepository.ContainsAsync( a => a.AudienceNumber == command.AudienceNumber && a.CorpuseId == command.CorpuseId) )
            {
                return ValidationResult.Fail("Аудитория в таком корпусе уже есть");
            }

            if ( command.Name == null || command.Name == String.Empty )
            {
                return ValidationResult.Fail( "Имя аудитории не должно быть пустым" );
            }

            if ( command.AudienceType > AudienceType.Other || command.AudienceType < AudienceType.Lecture )
            {
                return ValidationResult.Fail( "Такого типа аудитории нет" );
            }

            if ( command.Capacity <= 0 )
            {
                return ValidationResult.Fail( "Вместимость должна быть больше 0" );
            }

            if ( command.Floor <= 0 )
            {
                return ValidationResult.Fail( "Этаж должна быть больше 0" );
            }

            if ( command.AudienceNumber <= 0 )
            {
                return ValidationResult.Fail( "Номер аудитории должен быть больше 0" );
            }

            return ValidationResult.Ok();
        }
    }
}
