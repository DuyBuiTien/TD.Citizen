using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Covid;
using TD.CongDan.Domain.Entities.Ecommerce;
using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class DonViCongTacConfiguration : IEntityTypeConfiguration<DonViCongTac>
    {
        public void Configure(EntityTypeBuilder<DonViCongTac> builder)
        {
            builder.ToTable("DonViCongTacs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

        }
    }
}
