using Audiences.Application.Validation;
using Audiences.Domain.Repositories;
using static Audiences.Domain.AudienceTypeEnum;

namespace Audiences.Application.SQRSActions.Commands.UpdateAudience
{
    public class UpdateAudienceValidator : IAsyncValidator<UpdateAudienceCommand>
    {
        private readonly IAudienceRepository _audienceRepository;

        public UpdateAudienceValidator( IAudienceRepository audienceRepository )
        {
            _audienceRepository = audienceRepository;
        }

        public async Task<ValidationResult> ValidationAsync( UpdateAudienceCommand command )
        {
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

            if ( _audienceRepository.GetAudienceByIdAsync(command.Id).Result.AudienceNumber == command.AudienceNumber )
            {
                return ValidationResult.Ok();
            }
            else
            {
                if ( !await _audienceRepository.ContainsAsync( a=> a.CorpuseId == command.CorpuseId && a.AudienceNumber == command.AudienceNumber) )
                {
                    return ValidationResult.Ok();
                }
                return ValidationResult.Fail("Такая аудитория в корпусе уже есть");
            }
        }
    }
}
