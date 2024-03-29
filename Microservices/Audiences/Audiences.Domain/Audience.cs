using static Audiences.Domain.AudienceTypeEnum;

namespace Audiences.Domain
{
    public class Audience
    {
        public int Id { get; private set; }
        public int CorpuseId { get; private set; }
        public string Name { get; private set; }
        public AudienceType AudienceType { get; private set; }
        public int Capacity { get; private set; }
        public int Floor { get; private set; }
        public int AudienceNumber { get; private set; }

        public Audience( int corpuseId, string name, AudienceType audienceType, int capacity, int floor, int audienceNumber )
        {
            CorpuseId = corpuseId;
            Name = name;
            AudienceType = audienceType;
            Capacity = capacity;
            Floor = floor;
            AudienceNumber = audienceNumber;
        }

        public void Update( int corpuseId, string name, AudienceType audienceType, int capacity, int floor, int audienceNumber )
        {   
            CorpuseId = corpuseId;
            Name = name;
            AudienceType = audienceType;
            Capacity = capacity;
            Floor = floor;
            AudienceNumber = audienceNumber;
        }
    }
}
