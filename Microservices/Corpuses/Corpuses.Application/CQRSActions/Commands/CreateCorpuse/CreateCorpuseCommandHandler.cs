using Corpuses.Application.CQRSInterfaces;
using Corpuses.Application.Results;
using Corpuses.Application.Validation;
using Corpuses.Domain;
using Corpuses.Domain.Repositories;

namespace Corpuses.Application.CQRSActions.Commands.CreateCorpuse
{
    public class CreateCorpuseCommandHandler : ICommandHandler<CreateCorpuseCommand>
    {
        private readonly ICorpuseRepository _corpuseRepository;
        private readonly IAsyncValidator<CreateCorpuseCommand> _createCorpuseCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCorpuseCommandHandler(
            ICorpuseRepository corpuseRepository,
            IAsyncValidator<CreateCorpuseCommand> validator,
            IUnitOfWork unitOfWork)
        {
            _corpuseRepository = corpuseRepository;
            _createCorpuseCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync( CreateCorpuseCommand command )
        {
            ValidationResult validationResult = await _createCorpuseCommandValidator.ValidationAsync( command );
            if ( !validationResult.IsFail )
            {
                Corpuse corpuse = new Corpuse( command.Name, command.Address, command.FloorsNumber );
                _corpuseRepository.Add( corpuse );
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult( validationResult );
        }
    }
}
