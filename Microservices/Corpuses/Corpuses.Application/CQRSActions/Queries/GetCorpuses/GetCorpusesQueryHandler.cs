using System.Collections.Generic;
using System.Runtime.InteropServices;
using Corpuses.Application.CQRSActions.DTOs;
using Corpuses.Application.CQRSInterfaces;
using Corpuses.Application.Results;
using Corpuses.Application.Validation;
using Corpuses.Domain;
using Corpuses.Domain.Repositories;

namespace Corpuses.Application.CQRSActions.Queries.GetCorpuses
{
    public class GetCorpusesQueryHandler : IQueryHandler<IReadOnlyList<GetCorpusesQueryDto>, GetCorpusesQuery>
    {
        private readonly IAsyncValidator<GetCorpusesQuery> _getCorpusesQueryValidator;
        private readonly ICorpuseRepository _corpuseRepository;

        public GetCorpusesQueryHandler( IAsyncValidator<GetCorpusesQuery> Validator, ICorpuseRepository corpuseRepository )
        {
            _getCorpusesQueryValidator = Validator;
            _corpuseRepository = corpuseRepository;
        }

        public async Task<QueryResult<IReadOnlyList<GetCorpusesQueryDto>>> HandleAsync( GetCorpusesQuery query )
        {
            ValidationResult validationResult = await _getCorpusesQueryValidator.ValidationAsync( query );
            if ( validationResult.IsFail )
            {
                return new QueryResult<IReadOnlyList<GetCorpusesQueryDto>>(validationResult);
            }

            IReadOnlyList<Corpuse> corpuses = await _corpuseRepository.GetCorpusesAsync();

            IReadOnlyList<GetCorpusesQueryDto> getCorpusesQueryDtos = corpuses.Select( c => new GetCorpusesQueryDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                FloorsNumber = c.FloorsNumber
            } ).ToList();
            return new QueryResult<IReadOnlyList<GetCorpusesQueryDto>>( getCorpusesQueryDtos );
        }
    }
}
