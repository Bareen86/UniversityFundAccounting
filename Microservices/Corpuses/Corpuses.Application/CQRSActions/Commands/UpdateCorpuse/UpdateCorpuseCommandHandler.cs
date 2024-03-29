using Corpuses.Application.CQRSInterfaces;
using Corpuses.Application.Results;
using Corpuses.Application.Validation;
using Corpuses.Domain;
using Corpuses.Domain.Repositories;

namespace Corpuses.Application.CQRSActions.Commands.UpdateCorpuse
{
    public class UpdateCorpuseCommandHandler : ICommandHandler<UpdateCorpuseCommand>
    {
        private readonly ICorpuseRepository _corpuseRepository;
        private readonly IAsyncValidator<UpdateCorpuseCommand> _updateCorpuseCommandValidator;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCorpuseCommandHandler(
            ICorpuseRepository corpuseRepository,
            IAsyncValidator<UpdateCorpuseCommand> validator,
            IUnitOfWork unitOfWork )
        {
            _corpuseRepository = corpuseRepository;
            _updateCorpuseCommandValidator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> HandleAsync( UpdateCorpuseCommand command )
        {
            ValidationResult validationResult = await _updateCorpuseCommandValidator.ValidationAsync( command );
            if ( !validationResult.IsFail )
            {
                Corpuse corpuse = await _corpuseRepository.GetByIdAsync( command.Id );
                corpuse.Update(command.Name, command.Address, command.FloorsNumber);
                await _unitOfWork.CommitAsync();
            }
            return new CommandResult( validationResult );
        }
    }
}
