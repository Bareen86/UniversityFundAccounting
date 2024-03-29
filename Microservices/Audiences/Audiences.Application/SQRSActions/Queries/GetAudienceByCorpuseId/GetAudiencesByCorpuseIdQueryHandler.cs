using Audiences.Application.Results;
using Audiences.Application.SQRSActions.DTOs;
using Audiences.Application.SQRSInterfaces;
using Audiences.Application.Validation;
using Audiences.Domain;
using Audiences.Domain.Repositories;

namespace Audiences.Application.SQRSActions.Queries.GetAudienceByCorpuseId
{
    public class GetAudiencesByCorpuseIdQueryHandler : IQueryHandler<IReadOnlyList<GetAudiencesByCorpuseIdQueryDto>, GetAudiencesByCorpuseIdQuery>
    {
        private readonly IAudienceRepository _audienceRepository;
        private readonly IAsyncValidator<GetAudiencesByCorpuseIdQuery> _getAudienceByCorpuseIdQueryValidator;

        public GetAudiencesByCorpuseIdQueryHandler( IAudienceRepository audienceRepository, IAsyncValidator<GetAudiencesByCorpuseIdQuery> validator )
        {
            _audienceRepository = audienceRepository;
            _getAudienceByCorpuseIdQueryValidator = validator;
        }

        public async Task<QueryResult<IReadOnlyList<GetAudiencesByCorpuseIdQueryDto>>> HandleAsync( GetAudiencesByCorpuseIdQuery query )
        {
            ValidationResult validationResult = await _getAudienceByCorpuseIdQueryValidator.ValidationAsync( query );
            if ( validationResult.IsFail )
            {
                return new QueryResult<IReadOnlyList<GetAudiencesByCorpuseIdQueryDto>>( validationResult );
            }

            IReadOnlyList<Audience> audiences = await _audienceRepository.GetAudiencesByCorpuseIdAsync(query.Id);

            List<GetAudiencesByCorpuseIdQueryDto> getAudiencesByCorpuseIdQueryDtos = audiences.Select( a => new GetAudiencesByCorpuseIdQueryDto
            {
                Id = a.Id,
                CorpuseId = a.CorpuseId,
                Name = a.Name,
                AudienceType = a.AudienceType,
                Capacity = a.Capacity,
                Floor = a.Floor,
                AudienceNumber = a.AudienceNumber
            } ).ToList();
            return new QueryResult<IReadOnlyList<GetAudiencesByCorpuseIdQueryDto>>( getAudiencesByCorpuseIdQueryDtos );
        }
    }
}
