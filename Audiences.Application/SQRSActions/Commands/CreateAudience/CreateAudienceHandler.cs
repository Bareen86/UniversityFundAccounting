using Audiences.Application.Results;
using Audiences.Application.SQRSInterfaces;
using Audiences.Application.Validation;
using Audiences.Domain;
using Audiences.Domain.Repositories;

namespace Audiences.Application.SQRSActions.Commands.CreateAudience
{
    public class CreateAudienceHandler : ICommandHandler<CreateAudienceCommand>
    {
        private readonly IAudienceRepository _audienceRepository;
        private readonly IAsyncValidator<CreateAudienceCommand> _createAudienceValidator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAudienceHandler(
            IAudienceRepository audienceRepository,
            IAsyncValidator<CreateAudienceCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _audienceRepository = audienceRepository;
            _createAudienceValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync( CreateAudienceCommand command )
        {
            ValidationResult validationResult = await _createAudienceValidator.ValidationAsync( command );
            if ( !validationResult.IsFail )
            {
                Audience audience = new Audience( command.CorpuseId, command.Name,
                    command.AudienceType, command.Capacity, command.Floor, command.AudienceNumber);
                _audienceRepository.Add( audience );
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult(validationResult);
        }
    }
}
