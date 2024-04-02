using Audiences.Application.Results;
using Audiences.Application.SQRSActions.Commands.DeleteAudienceByCorpuseId;
using Audiences.Application.SQRSInterfaces;
using MassTransit;
using UniversityFundAccounting.Shared;

namespace Audiences.Api.Consumers
{
    public class AudienceConsumer : IConsumer<DeleteByCorpuseIdDto>
    {
        private readonly ICommandHandler<DeleteAudiencesByCorpuseIdCommand> _deleteAudiencesByCorpuseIdHandler;

        public AudienceConsumer( ICommandHandler<DeleteAudiencesByCorpuseIdCommand> deleteAudiencesByCorpuseIdHandler )
        {
            _deleteAudiencesByCorpuseIdHandler = deleteAudiencesByCorpuseIdHandler;
        }

        public async Task Consume( ConsumeContext<DeleteByCorpuseIdDto> context )
        {
            DeleteAudiencesByCorpuseIdCommand deleteAudienceByCorpuseIdCommand = new DeleteAudiencesByCorpuseIdCommand()
            {
                Id = context.Message.Id
            };
            CommandResult commandResult = await _deleteAudiencesByCorpuseIdHandler.HandleAsync( deleteAudienceByCorpuseIdCommand );
        }
    }
}
