using Audiences.Application.Results;
using Audiences.Application.SQRSInterfaces;
using Audiences.Application.Validation;
using Audiences.Domain;
using Audiences.Domain.Repositories;

namespace Audiences.Application.SQRSActions.Commands.DeleteAudience
{
    public class DeleteAudienceHandler : ICommandHandler<DeleteAudienceCommand>
    {
        private readonly IAudienceRepository _audienceRepository;
        private readonly IAsyncValidator<DeleteAudienceCommand> _deleteAudienceValidator;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAudienceHandler(
            IAudienceRepository audienceRepository,
            IAsyncValidator<DeleteAudienceCommand> validator,
            IUnitOfWork unitOfWork )
        {
            _audienceRepository = audienceRepository;
            _deleteAudienceValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync( DeleteAudienceCommand command )
        {
            ValidationResult validationResult = await _deleteAudienceValidator.ValidationAsync( command );
            if ( !validationResult.IsFail )
            {
                Audience audience = await _audienceRepository.GetAudienceByIdAsync( command.Id );
                _audienceRepository.Delete( audience );
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult( validationResult );
        }
    }
}
