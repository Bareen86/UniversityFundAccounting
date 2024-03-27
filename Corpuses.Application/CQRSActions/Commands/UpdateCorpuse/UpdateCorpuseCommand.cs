namespace Corpuses.Application.CQRSActions.Commands.UpdateCorpuse
{
    public class UpdateCorpuseCommand
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int FloorsNumber { get; set; }
    }
}
