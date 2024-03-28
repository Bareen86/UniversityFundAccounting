namespace Corpuses.Application.CQRSActions.Commands.UpdateCorpuse
{
    public class UpdateCorpuseCommand
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public int FloorsNumber { get; init; }
    }
}
