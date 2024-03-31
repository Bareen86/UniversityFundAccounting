namespace Corpuses.Application.CQRSActions.DTOs
{
    public class GetCorpusesQueryDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public int FloorsNumber { get; init; }
    }
}
