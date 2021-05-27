using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Covid;
using TD.CongDan.Domain.Entities.Ecommerce;
using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class NguoiKhaiBaoConfiguration : IEntityTypeConfiguration<NguoiKhaiBao>
    {
        public void Configure(EntityTypeBuilder<NguoiKhaiBao> builder)
        {
            builder.ToTable("NguoiKhaiBaos");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne<Area>(s => s.Province).WithMany(g => g.NguoiKhaiBaoProvinces).HasForeignKey(s => s.ProvinceId);
            builder.HasOne<Area>(s => s.District).WithMany(g => g.NguoiKhaiBaoDistricts).HasForeignKey(s => s.DistrictId);
            builder.HasOne<Area>(s => s.Commune).WithMany(g => g.NguoiKhaiBaoCommunes).HasForeignKey(s => s.CommuneId);
            builder.HasOne<Gender>(s => s.Gender).WithMany(g => g.NguoiKhaiBaos).HasForeignKey(s => s.GenderId);
            builder.HasOne<QuocGia>(s => s.QuocGia).WithMany(g => g.NguoiKhaiBaos).HasForeignKey(s => s.NationalityId);


        }
    }
}
