using Audiences.Application.Results;

namespace Audiences.Application.SQRSInterfaces
{
    public interface ICommandHandler<TCommand> where TCommand : class
    {
        Task<CommandResult> HandleAsync( TCommand command );
    }
}
