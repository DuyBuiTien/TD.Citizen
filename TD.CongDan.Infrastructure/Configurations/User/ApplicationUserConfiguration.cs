using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");
            builder.HasOne<Gender>(s => s.Gender).WithMany(g => g.UserInfos).HasForeignKey(s => s.GenderId);
            builder.HasOne<Religion>(s => s.Religion).WithMany(g => g.UserInfos).HasForeignKey(s => s.ReligionId);
            builder.HasOne<IdentityType>(s => s.IdentityType).WithMany(g => g.UserInfos).HasForeignKey(s => s.IdentityTypeId);
            builder.HasOne<MaritalStatus>(s => s.MaritalStatus).WithMany(g => g.UserInfos).HasForeignKey(s => s.MaritalStatusId);
            builder.HasOne<Area>(s => s.Province).WithMany(g => g.UserInfoProvinces).HasForeignKey(s => s.ProvinceId);
            builder.HasOne<Area>(s => s.District).WithMany(g => g.UserInfoDistricts).HasForeignKey(s => s.DistrictId);
            builder.HasOne<Area>(s => s.Commune).WithMany(g => g.UserInfoCommunes).HasForeignKey(s => s.CommuneId);

            builder.HasOne<ChucVu>(s => s.ChucVu).WithMany(g => g.ApplicationUsers).HasForeignKey(s => s.ChucVuId);

            builder.HasOne<DonViCongTac>(s => s.DonViCongTac).WithMany(g => g.ApplicationUsers).HasForeignKey(s => s.DonViCongTacId);

            builder.HasOne<ChotKiemDich>(s => s.ChotKiemDich).WithMany(g => g.ApplicationUsers).HasForeignKey(s => s.ChotKiemDichId);
        }
    }
}
