using Audiences.Domain.Repositories.BasicRepositories;
using Audiences.Infrastructure.Foundation;
using Microsoft.EntityFrameworkCore;

namespace Audiences.Infrastructure
{
    public abstract class BasicRepository<TEntity> : IAddedRepository<TEntity>,
        IRemovableRepository<TEntity> where TEntity : class
    {
        private readonly AudiencesDbContext _dbContext;
        public DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

        public BasicRepository( AudiencesDbContext dbContext )
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
