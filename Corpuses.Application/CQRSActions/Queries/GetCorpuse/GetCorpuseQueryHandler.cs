using Corpuses.Application.CQRSActions.DTOs;
using Corpuses.Application.CQRSInterfaces;
using Corpuses.Application.Results;
using Corpuses.Application.Validation;
using Corpuses.Domain;
using Corpuses.Domain.Repositories;

namespace Corpuses.Application.CQRSActions.Queries.GetCorpuse
{
    public class GetCorpuseQueryHandler : IQueryHandler<GetCorpuseQueryDto, GetCorpuseQuery>
    {
        private readonly ICorpuseRepository _corpuseRepository;
        private readonly IAsyncValidator<GetCorpuseQuery> _getCorpuseQueryValidator;

        public GetCorpuseQueryHandler( ICorpuseRepository corpuseRepository, IAsyncValidator<GetCorpuseQuery> validator )
        {
            _corpuseRepository = corpuseRepository;
            _getCorpuseQueryValidator = validator;
        }

        public async Task<QueryResult<GetCorpuseQueryDto>> HandleAsync( GetCorpuseQuery query )
        {
            ValidationResult validationResult = await _getCorpuseQueryValidator.ValidationAsync( query );
            if ( validationResult.IsFail )
            {
                return new QueryResult<GetCorpuseQueryDto>( validationResult );
            }

            Corpuse corpuse = await _corpuseRepository.GetByIdAsync( query.Id );

            GetCorpuseQueryDto getCorpuseQueryDto = new GetCorpuseQueryDto()
            {
                Id = corpuse.Id,
                Name = corpuse.Name,
                Address = corpuse.Address,
                FloorsNumber = corpuse.FloorsNumber
            };
            return new QueryResult<GetCorpuseQueryDto>( getCorpuseQueryDto );
        }
    }
}
