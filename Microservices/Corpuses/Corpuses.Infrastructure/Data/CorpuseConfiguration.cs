using Corpuses.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Corpuses.Infrastructure.Data
{
    internal class CorpuseConfiguration : IEntityTypeConfiguration<Corpuse>
    {
        public void Configure( EntityTypeBuilder<Corpuse> builder )
        {
            builder.ToTable("Corpuses")
                .HasKey( x => x.Id );

            builder.Property( x => x.Id ).ValueGeneratedOnAdd();
            builder.Property( x => x.Name ).HasMaxLength( 100 ).IsRequired();
            builder.Property( x => x.Address).HasMaxLength( 300 ).IsRequired();
        }
    }
}
