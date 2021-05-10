using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class IdentityTypeConfiguration : IEntityTypeConfiguration<IdentityType>
    {
        public void Configure(EntityTypeBuilder<IdentityType> builder)
        {
            builder.ToTable("IdentityTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();

        }
    }
}
