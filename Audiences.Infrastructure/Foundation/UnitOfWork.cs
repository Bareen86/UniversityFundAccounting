using Audiences.Application;

namespace Audiences.Infrastructure.Foundation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AudiencesDbContext _dbContext;

        public UnitOfWork( AudiencesDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
