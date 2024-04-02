using Audiences.Api.Dtos;
using Audiences.Application.Results;
using Audiences.Application.SQRSActions.Commands.CreateAudience;
using Audiences.Application.SQRSActions.Commands.DeleteAudience;
using Audiences.Application.SQRSActions.Commands.UpdateAudience;
using Audiences.Application.SQRSActions.DTOs;
using Audiences.Application.SQRSActions.Queries.GetAudienceByCorpuseId;
using Audiences.Application.SQRSInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Audiences.Api.Controllers
{
    [Route( "api/audiences" )]
    [ApiController]
    public class AudiencesController : ControllerBase
    {
        private readonly ICommandHandler<CreateAudienceCommand> _createAudienceCommandHandler;
        private readonly ICommandHandler<UpdateAudienceCommand> _updateAudienceCommandHandler;
        private readonly ICommandHandler<DeleteAudienceCommand> _deleteAudienceCommandHandler;
        private readonly IQueryHandler<IReadOnlyList<GetAudiencesByCorpuseIdQueryDto>, GetAudiencesByCorpuseIdQuery> _getAudiencesByCorpuseIdQueryHandler;

        public AudiencesController(
            ICommandHandler<CreateAudienceCommand> createAudienceCommandHandler,
            ICommandHandler<UpdateAudienceCommand> updateAudienceCommandHandler,
            ICommandHandler<DeleteAudienceCommand> deleteAudienceCommandHandler,
            IQueryHandler<IReadOnlyList<GetAudiencesByCorpuseIdQueryDto>, GetAudiencesByCorpuseIdQuery> getAudiencesByCorpuseIdQueryHandler )
        {
            _createAudienceCommandHandler = createAudienceCommandHandler;
            _updateAudienceCommandHandler = updateAudienceCommandHandler;
            _deleteAudienceCommandHandler = deleteAudienceCommandHandler;
            _getAudiencesByCorpuseIdQueryHandler = getAudiencesByCorpuseIdQueryHandler;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAudience( [FromBody] CreateAudienceDto createAudienceDto )
        {
            CreateAudienceCommand createAudienceCommand = new CreateAudienceCommand()
            {
                CorpuseId = createAudienceDto.CorpuseId,
                Name = createAudienceDto.Name,
                AudienceType = createAudienceDto.AudienceType,
                Capacity = createAudienceDto.Capacity,
                Floor = createAudienceDto.Floor,
                AudienceNumber = createAudienceDto.AudienceNumber,
            };
            CommandResult commandResult = await _createAudienceCommandHandler.HandleAsync( createAudienceCommand );

            if ( commandResult.ValidationResult.IsFail )
            {
                return BadRequest( commandResult );
            }
            return Ok( commandResult );
        }

        [HttpDelete("{audienceId}")]
        public async Task<IActionResult> DeleteAudience( [FromRoute] int audienceId )
        {
            DeleteAudienceCommand deleteAudienceCommand = new DeleteAudienceCommand()
            {
                Id = audienceId
            };
            CommandResult commandResult = await _deleteAudienceCommandHandler.HandleAsync( deleteAudienceCommand );

            if ( commandResult.ValidationResult.IsFail )
            {
                return BadRequest( commandResult );
            }
            return Ok(commandResult);
        }

        [HttpPut( "{audienceId}" )]
        public async Task<IActionResult> UpdateAudience( [FromRoute] int audienceId, [FromBody] UpdateAudienceDto updateAudienceDto )
        {
            UpdateAudienceCommand updateAudienceCommand = new UpdateAudienceCommand()
            {
                Id = audienceId,
                CorpuseId = updateAudienceDto.CorpuseId,
                Name = updateAudienceDto.Name,
                AudienceType = updateAudienceDto.AudienceType,
                Capacity = updateAudienceDto.Capacity,
                Floor = updateAudienceDto.Floor,
                AudienceNumber = updateAudienceDto.AudienceNumber
            };
            CommandResult commandResult = await _updateAudienceCommandHandler.HandleAsync( updateAudienceCommand );

            if ( commandResult.ValidationResult.IsFail )
            {
                return BadRequest( commandResult );
            }
            return Ok( commandResult );
        }

        [HttpGet( "{corpuseId}" )]
        public async Task<IActionResult> GetAudiences( [FromRoute] int corpuseId )
        {
            GetAudiencesByCorpuseIdQuery getAudiencesByCorpuseIdQuery = new GetAudiencesByCorpuseIdQuery()
            {
                Id = corpuseId
            };
            QueryResult<IReadOnlyList<GetAudiencesByCorpuseIdQueryDto>> queryResult = await _getAudiencesByCorpuseIdQueryHandler.HandleAsync( getAudiencesByCorpuseIdQuery );

            if ( queryResult.ValidationResult.IsFail )
            {
                return BadRequest( queryResult );
            }
            return Ok( queryResult );
        }
    }
}
