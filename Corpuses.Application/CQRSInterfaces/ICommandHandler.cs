using Corpuses.Application.Results;

namespace Corpuses.Application.CQRSInterfaces
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task<CommandResult> HandleAsync(TCommand command);
    }
}
