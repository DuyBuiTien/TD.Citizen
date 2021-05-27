using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Covid;
using TD.CongDan.Domain.Entities.Ecommerce;
using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class ChotKiemDichConfiguration : IEntityTypeConfiguration<ChotKiemDich>
    {
        public void Configure(EntityTypeBuilder<ChotKiemDich> builder)
        {
            builder.ToTable("ChotKiemDichs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne<Area>(s => s.Province).WithMany(g => g.ChotKiemDichProvinces).HasForeignKey(s => s.ProvinceId);
            builder.HasOne<Area>(s => s.District).WithMany(g => g.ChotKiemDichDistricts).HasForeignKey(s => s.DistrictId);
            builder.HasOne<Area>(s => s.Commune).WithMany(g => g.ChotKiemDichCommunes).HasForeignKey(s => s.CommuneId);
        }

    }
}
