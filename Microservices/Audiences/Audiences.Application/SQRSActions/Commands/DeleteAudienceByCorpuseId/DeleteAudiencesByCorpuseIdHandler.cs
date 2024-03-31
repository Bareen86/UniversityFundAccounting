using Audiences.Application.Results;
using Audiences.Application.SQRSInterfaces;
using Audiences.Application.Validation;
using Audiences.Domain.Repositories;

namespace Audiences.Application.SQRSActions.Commands.DeleteAudienceByCorpuseId
{
    public class DeleteAudienceByCorpuseIdHandler : ICommandHandler<DeleteAudiencesByCorpuseIdCommand>
    {
        private readonly IAudienceRepository _audienceRepository;
        private readonly IAsyncValidator<DeleteAudiencesByCorpuseIdCommand> _deleteAudienceByCorpuseIdCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAudienceByCorpuseIdHandler(
            IAudienceRepository audienceRepository,
            IAsyncValidator<DeleteAudiencesByCorpuseIdCommand> validator,
            IUnitOfWork unitOfWork )
        {
            _audienceRepository = audienceRepository;
            _deleteAudienceByCorpuseIdCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync( DeleteAudiencesByCorpuseIdCommand command )
        {
            ValidationResult validationResult = await _deleteAudienceByCorpuseIdCommandValidator.ValidationAsync( command );
            if ( !validationResult.IsFail )
            {
                await _audienceRepository.DeleteAudiencesByCorpuseIdAsync( command.Id );
                await _unitOfWork.CommitAsync();
                return new CommandResult( validationResult );
            }
            return new CommandResult( validationResult );
        }
    }
}
