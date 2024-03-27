namespace Corpuses.Application.CQRSActions.Commands.CreateCorpuse
{
    public class CreateCorpuseCommand
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int FloorsNumber { get; set; }
    }
}
