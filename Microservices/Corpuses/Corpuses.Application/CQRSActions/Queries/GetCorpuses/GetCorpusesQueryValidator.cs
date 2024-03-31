using Corpuses.Application.Validation;

namespace Corpuses.Application.CQRSActions.Queries.GetCorpuses
{
    public class GetCorpusesQueryValidator : IAsyncValidator<GetCorpusesQuery>
    {
        public async Task<ValidationResult> ValidationAsync( GetCorpusesQuery inputData )
        {
            return ValidationResult.Ok();
        }
    }
}
