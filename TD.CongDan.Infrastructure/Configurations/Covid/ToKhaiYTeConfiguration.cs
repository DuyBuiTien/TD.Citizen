using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Covid;
using TD.CongDan.Domain.Entities.Ecommerce;
using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class ToKhaiYTeConfiguration : IEntityTypeConfiguration<ToKhaiYTe>
    {
        public void Configure(EntityTypeBuilder<ToKhaiYTe> builder)
        {
            builder.ToTable("ToKhaiYTes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.HasOne<Area>(s => s.Province).WithMany(g => g.ToKhaiYTeProvinces).HasForeignKey(s => s.ProvinceId);
            builder.HasOne<Area>(s => s.District).WithMany(g => g.ToKhaiYTeDistricts).HasForeignKey(s => s.DistrictId);
            builder.HasOne<Area>(s => s.Commune).WithMany(g => g.ToKhaiYTeCommunes).HasForeignKey(s => s.CommuneId);

            builder.HasOne<NguoiKhaiBao>(s => s.NguoiKhaiBao).WithMany(g => g.ToKhaiYTes).HasForeignKey(s => s.NguoiKhaiBaoId);
            builder.HasOne<ChotKiemDich>(s => s.ChotKiemDich).WithMany(g => g.ToKhaiYTes).HasForeignKey(s => s.ChotKiemDichId);

            builder.HasOne<PhuongTien>(s => s.PhuongTien).WithMany(g => g.ToKhaiYTes).HasForeignKey(s => s.ChotKiemDichId);
        }
    }
}
