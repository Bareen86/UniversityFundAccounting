using Audiences.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audiences.Infrastructure.Data
{
    internal class AudienceConfiguration : IEntityTypeConfiguration<Audience>
    {
        public void Configure( EntityTypeBuilder<Audience> builder )
        {
            builder.ToTable( "Audiences" )
                .HasKey( x => x.Id );

            builder.HasIndex( x => x.CorpuseId );
            builder.Property( x => x.Id ).ValueGeneratedOnAdd();
            builder.Property( x => x.CorpuseId ).IsRequired();
            builder.Property( x => x.Name ).HasMaxLength( 100 ).IsRequired();
            builder.Property( x => x.AudienceType ).IsRequired();
            builder.Property( x => x.Capacity ).IsRequired();
            builder.Property( x => x.Floor ).IsRequired();
            builder.Property( x => x.AudienceNumber ).IsRequired();
        }
    }
}
