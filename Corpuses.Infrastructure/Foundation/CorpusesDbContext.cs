using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Corpuses.Infrastructure.Foundation
{
    public class CorpusesDbContext : DbContext
    {
        public CorpusesDbContext( DbContextOptions options ) : base( options )
        {

        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.ApplyConfigurationsFromAssembly( Assembly.GetExecutingAssembly() );
        }
    }
}
