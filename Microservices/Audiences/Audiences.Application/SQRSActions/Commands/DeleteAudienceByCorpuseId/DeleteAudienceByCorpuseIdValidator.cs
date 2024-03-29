using Audiences.Application.Validation;

namespace Audiences.Application.SQRSActions.Commands.DeleteAudienceByCorpuseId
{
    public class DeleteAudienceByCorpuseIdValidator : IAsyncValidator<DeleteAudienceByCorpuseIdCommand>
    {
        public DeleteAudienceByCorpuseIdValidator()
        {
            
        }

        public Task<ValidationResult> ValidationAsync( DeleteAudienceByCorpuseIdCommand inputData )
        {
            throw new NotImplementedException();
        }
    }
}
