using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class LicensePlateConfiguration : IEntityTypeConfiguration<LicensePlate>
    {
        public void Configure(EntityTypeBuilder<LicensePlate> builder)
        {
            builder.ToTable("LicensePlates");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
          

        }
    }
}
