using Audiences.Application.Results;
using Audiences.Application.SQRSActions.DTOs;
using Audiences.Application.SQRSActions.Queries.GetAudienceByCorpuseId;
using Audiences.Application.SQRSInterfaces;
using Audiences.Application.Validation;
using Audiences.Domain;
using Audiences.Domain.Repositories;

namespace Audiences.Application.SQRSActions.Queries.GetAudienceById
{
    public class GetAudienceByIdHandler : IQueryHandler<GetAudienceByIdQueryDto, GetAudiencesByCorpuseIdQuery>
    {
        private readonly IAudienceRepository _audienceRepository;
        private readonly IAsyncValidator<GetAudiencesByCorpuseIdQuery> _getAudienceByCorpuseIdValidator;

        public GetAudienceByIdHandler( IAudienceRepository audienceRepository, IAsyncValidator<GetAudiencesByCorpuseIdQuery> asyncValidator )
        {
            _audienceRepository = audienceRepository;
            _getAudienceByCorpuseIdValidator = asyncValidator;
        }

        async Task<QueryResult<GetAudienceByIdQueryDto>> IQueryHandler<GetAudienceByIdQueryDto, GetAudiencesByCorpuseIdQuery>.HandleAsync( GetAudiencesByCorpuseIdQuery query )
        {
            ValidationResult validationResult = await _getAudienceByCorpuseIdValidator.ValidationAsync( query );
            if ( validationResult.IsFail )
            {
                return new QueryResult<GetAudienceByIdQueryDto>( validationResult );
            }
            
            Audience audience = await _audienceRepository.GetAudienceByIdAsync( query.Id );

            GetAudienceByIdQueryDto getAudienceByIdQueryDto = new GetAudienceByIdQueryDto()
            {
                Id = audience.Id,
                CorpuseId = audience.CorpuseId,
                Name = audience.Name,
                AudienceType = audience.AudienceType,
                Capacity = audience.Capacity,
                Floor = audience.Floor,
                AudienceNumber = audience.AudienceNumber
            };
            return new QueryResult<GetAudienceByIdQueryDto>( getAudienceByIdQueryDto );
        }
    }
}
