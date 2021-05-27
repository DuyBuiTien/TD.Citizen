using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Covid;
using TD.CongDan.Domain.Entities.Ecommerce;
using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class QuocGiaConfiguration : IEntityTypeConfiguration<QuocGia>
    {
        public void Configure(EntityTypeBuilder<QuocGia> builder)
        {
            builder.ToTable("QuocGias");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

        }
    }
}
