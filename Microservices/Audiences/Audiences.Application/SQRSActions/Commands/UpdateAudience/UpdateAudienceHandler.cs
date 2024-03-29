using Audiences.Application.Results;
using Audiences.Application.SQRSInterfaces;
using Audiences.Application.Validation;
using Audiences.Domain;
using Audiences.Domain.Repositories;

namespace Audiences.Application.SQRSActions.Commands.UpdateAudience
{
    public class UpdateAudienceHandler : ICommandHandler<UpdateAudienceCommand>
    {
        private readonly IAudienceRepository _audienceRepository;
        private readonly IAsyncValidator<UpdateAudienceCommand> _updateAudienceValidator;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAudienceHandler(
            IAudienceRepository audienceRepository,
            IAsyncValidator<UpdateAudienceCommand> validator,
            IUnitOfWork unitOfWork )
        {
            _audienceRepository = audienceRepository;
            _updateAudienceValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync( UpdateAudienceCommand command )
        {
            ValidationResult validationResult = await _updateAudienceValidator.ValidationAsync( command );
            if ( !validationResult.IsFail )
            {   
                Audience audience = await _audienceRepository.GetAudienceByIdAsync( command.Id );
                audience.Update( command.CorpuseId, command.Name,
                    command.AudienceType, command.Capacity, command.Floor, command.AudienceNumber );
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult( validationResult );
        }
    }
}
