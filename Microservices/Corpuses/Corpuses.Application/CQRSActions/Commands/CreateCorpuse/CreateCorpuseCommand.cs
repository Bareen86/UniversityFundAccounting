namespace Corpuses.Application.CQRSActions.Commands.CreateCorpuse
{
    public class CreateCorpuseCommand
    {
        public string Name { get; init; }
        public string Address { get; init; }
        public int FloorsNumber { get; init; }
    }
}
