using Corpuses.Api.Dtos;
using Corpuses.Application.CQRSActions.Commands.CreateCorpuse;
using Corpuses.Application.CQRSActions.Commands.DeleteCorpuse.DeleteCorpuseCommand;
using Corpuses.Application.CQRSActions.Commands.UpdateCorpuse;
using Corpuses.Application.CQRSActions.DTOs;
using Corpuses.Application.CQRSActions.Queries.GetCorpuse;
using Corpuses.Application.CQRSActions.Queries.GetCorpuses;
using Corpuses.Application.CQRSInterfaces;
using Corpuses.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace Corpuses.Api.Controllers
{
    [Route( "api/corpuses" )]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICommandHandler<CreateCorpuseCommand> _createCorpuseCommandHandler;
        private readonly ICommandHandler<UpdateCorpuseCommand> _updateCorpuseCommandHandler;
        private readonly ICommandHandler<DeleteCorpuseCommand> _deleleCorpuseCommandHandler;
        private readonly IQueryHandler<GetCorpuseQueryDto, GetCorpuseQuery> _getCorpuseQueryHandler;
        private readonly IQueryHandler<IReadOnlyList<GetCorpusesQueryDto>, GetCorpusesQuery> _getCorpusesQueryHandler;

        public CoursesController(
            ICommandHandler<CreateCorpuseCommand> createCorpuseCommandHandler,
            ICommandHandler<UpdateCorpuseCommand> updateCorpuseCommandHandler,
            ICommandHandler<DeleteCorpuseCommand> deleleCorpuseCommandHandler,
            IQueryHandler<GetCorpuseQueryDto, GetCorpuseQuery> getCorpuseQueryHandler,
            IQueryHandler<IReadOnlyList<GetCorpusesQueryDto>, GetCorpusesQuery> getCorpusesQueryHandler )
        {
            _createCorpuseCommandHandler = createCorpuseCommandHandler;
            _updateCorpuseCommandHandler = updateCorpuseCommandHandler;
            _deleleCorpuseCommandHandler = deleleCorpuseCommandHandler;
            _getCorpuseQueryHandler = getCorpuseQueryHandler;
            _getCorpusesQueryHandler = getCorpusesQueryHandler;
        }


        [HttpPost]
        public async Task<IActionResult> CreateCorpuse( [FromBody] CreateCorpuseDto createCorpuseRequest )
        {
            CreateCorpuseCommand createCorpuseCommand = new CreateCorpuseCommand()
            {
                Name = createCorpuseRequest.Name,
                Address = createCorpuseRequest.Address,
                FloorsNumber = createCorpuseRequest.FloorsNumber,
            };
            CommandResult commandResult = await _createCorpuseCommandHandler.HandleAsync( createCorpuseCommand );

            if ( commandResult.ValidationResult.IsFail )
            {
                return BadRequest( commandResult );
            }
            return Ok( commandResult );
        }

        [HttpGet]
        public async Task<IActionResult> GetCorpuses()
        {
            GetCorpusesQuery getCorpusesQuery = new GetCorpusesQuery();
            QueryResult<IReadOnlyList<GetCorpusesQueryDto>> queryResult = await _getCorpusesQueryHandler.HandleAsync( getCorpusesQuery );

            if ( queryResult.ValidationResult.IsFail )
            {
                return BadRequest( queryResult );
            }
            return Ok( queryResult );
        }

        [HttpGet( "{corpuseId}" )]
        public async Task<IActionResult> GetCorpuse( [FromRoute] int corpuseId )
        {
            GetCorpuseQuery getCorpuseQuery = new GetCorpuseQuery()
            {
                Id = corpuseId
            };
            QueryResult<GetCorpuseQueryDto> queryResult = await _getCorpuseQueryHandler.HandleAsync( getCorpuseQuery );

            if ( queryResult.ValidationResult.IsFail )
            {
                return BadRequest( queryResult );
            }
            return Ok( queryResult );
        }

        [HttpDelete( "{corpuseId}" )]
        public async Task<IActionResult> DeleteCorpuse( [FromRoute] int corpuseId )
        {
            DeleteCorpuseCommand deleteCorpuseCommand = new DeleteCorpuseCommand()
            {
                Id = corpuseId
            };
            CommandResult commandResult = await _deleleCorpuseCommandHandler.HandleAsync( deleteCorpuseCommand );

            if ( commandResult.ValidationResult.IsFail )
            {
                return BadRequest( commandResult );
            }
            return Ok( commandResult );
        }

        [HttpPut( "{corpuseId}" )]
        public async Task<IActionResult> UpdateCorpuse( [FromRoute] int corpuseId, UpdateCorpuseDto updateCorpuseRequest )
        {
            UpdateCorpuseCommand updateCorpuseCommand = new UpdateCorpuseCommand()
            {
                Id = corpuseId,
                Name = updateCorpuseRequest.Name,
                Address = updateCorpuseRequest.Address,
                FloorsNumber = updateCorpuseRequest.FloorsNumber
            };
            CommandResult commandResult = await _updateCorpuseCommandHandler.HandleAsync( updateCorpuseCommand );

            if ( commandResult.ValidationResult.IsFail )
            {
                return BadRequest( commandResult );
            }
            return Ok( commandResult );
        }
    }
}
