using Corpuses.Domain.Repositories.BasicRepositories;
using Corpuses.Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Corpuses.Infrastructure
{
    public abstract class BasicRepository<TEntity> : IAddedRepository<TEntity>,
        IRemovableRepository<TEntity> where TEntity : class
    {
        private readonly CorpusesDbContext _dbContext;
        public DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

        public BasicRepository( CorpusesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add( TEntity entity )
        {
            Entities.Add( entity );
        }

        public void Delete( TEntity entity )
        {
            Entities.Remove( entity );
        }
    }
}
