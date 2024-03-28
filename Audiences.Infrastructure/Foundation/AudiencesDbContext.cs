using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Audiences.Infrastructure.Foundation
{
    public class AudiencesDbContext : DbContext
    {
        public AudiencesDbContext( DbContextOptions options ) : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.ApplyConfigurationsFromAssembly( Assembly.GetExecutingAssembly() );
        }
    }
}
