using Audiences.Domain;
using Audiences.Domain.Repositories;
using Audiences.Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Audiences.Infrastructure.Data
{
    public class AudienceRepository : BasicRepository<Audience>, IAudienceRepository
    {
        public AudienceRepository( AudiencesDbContext dbContext ) : base( dbContext )
        {
        }

        public async Task<bool> CorpuseIsExist( int corpuseId )
        {
            List<Audience> audiences = await Entities.Where( a => a.CorpuseId == corpuseId ).ToListAsync();
            if (audiences.Count > 0 ) return true;
            return false;
        }

        public async Task DeleteAudiencesByCorpuseIdAsync( int id )
        {
            IReadOnlyList<Audience> audiences = await GetAudiencesByCorpuseIdAsync( id );
            Entities.RemoveRange( audiences );
        }
            
        public async Task<Audience> GetAudienceByAudienceNumberAsync( int audienceNumber )
        {
            return await Entities.Where( x => x.AudienceNumber == audienceNumber ).FirstOrDefaultAsync();
        }

        public async Task<Audience> GetAudienceByIdAsync( int id )
        {
            return await Entities.Where( x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Audience>> GetAudiencesByCorpuseIdAsync( int corpuseId )
        {
            return await Entities.Where( x => x.CorpuseId == corpuseId ).ToListAsync();
        }
    }
}
