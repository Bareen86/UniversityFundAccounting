namespace Corpuses.Application.CQRSActions.DTOs
{
    public class GetCorpuseQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int FloorsNumber { get; set; }
    }
}
