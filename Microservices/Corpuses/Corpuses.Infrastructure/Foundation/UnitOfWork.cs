using Corpuses.Application;

namespace Corpuses.Infrastructure.Foundation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CorpusesDbContext _dbContext;

        public UnitOfWork( CorpusesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
