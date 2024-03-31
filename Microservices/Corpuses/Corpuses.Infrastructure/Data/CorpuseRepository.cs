using System.Linq.Expressions;
using Corpuses.Domain;
using Corpuses.Domain.Repositories;
using Corpuses.Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Corpuses.Infrastructure.Data
{
    internal class CorpuseRepository : BasicRepository<Corpuse>, ICorpuseRepository
    {

        public CorpuseRepository( CorpusesDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> ContainsAsync( Expression<Func<Corpuse, bool>> predicate )
        {
            return await Entities.Where( predicate ).FirstOrDefaultAsync() != null;
        }

        public async Task<Corpuse> GetByIdAsync( int id )
        {
            return await Entities.Where( x => x.Id == id ).FirstOrDefaultAsync();
        }

        public async Task<Corpuse> GetByNameAndAddressAsync( string name, string address )
        {
            return await Entities.Where( x => x.Name == name && x.Address == address ).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Corpuse>> GetCorpusesAsync()
        {
            return await Entities.ToListAsync();
        }
    }
}
