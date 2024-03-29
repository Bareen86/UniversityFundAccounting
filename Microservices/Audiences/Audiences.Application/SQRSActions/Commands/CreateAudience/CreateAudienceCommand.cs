using static Audiences.Domain.AudienceTypeEnum;

namespace Audiences.Application.SQRSActions.Commands.CreateAudience
{
    public class CreateAudienceCommand
    {
        public int CorpuseId { get; init; }
        public string Name { get; init; }
        public AudienceType AudienceType { get; init; }
        public int Capacity { get; init; }
        public int Floor { get; init; }
        public int AudienceNumber { get; init; }
    }
}
