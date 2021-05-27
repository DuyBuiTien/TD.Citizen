using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Covid;
using TD.CongDan.Domain.Entities.Ecommerce;
using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class PhuongTienConfiguration : IEntityTypeConfiguration<PhuongTien>
    {
        public void Configure(EntityTypeBuilder<PhuongTien> builder)
        {
            builder.ToTable("PhuongTiens");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

        }
    }
}
