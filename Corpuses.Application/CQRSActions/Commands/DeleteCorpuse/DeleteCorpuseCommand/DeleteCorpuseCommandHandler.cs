using Corpuses.Application.CQRSInterfaces;
using Corpuses.Application.Results;
using Corpuses.Application.Validation;
using Corpuses.Domain;
using Corpuses.Domain.Repositories;

namespace Corpuses.Application.CQRSActions.Commands.DeleteCorpuse.DeleteCorpuseCommand
{
    public class DeleteCorpuseCommandHandler : ICommandHandler<DeleteCorpuseCommand>
    {
        private readonly ICorpuseRepository _corpuseRepository;
        private readonly IAsyncValidator<DeleteCorpuseCommand> _deleteCorpuseCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCorpuseCommandHandler(
            ICorpuseRepository corpuseRepository,
            IAsyncValidator<DeleteCorpuseCommand> validator,
            IUnitOfWork unitOfWork )
        {
            _corpuseRepository = corpuseRepository;
            _deleteCorpuseCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync( DeleteCorpuseCommand command )
        {
            ValidationResult validationResult = await _deleteCorpuseCommandValidator.ValidationAsync( command );
            if ( !validationResult.IsFail )
            {
                Corpuse corpuse = await _corpuseRepository.GetByIdAsync( command.Id );
                _corpuseRepository.Delete(corpuse );
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult( validationResult );
        }
    }
}
