using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Covid;


namespace TD.CongDan.Infrastructure.Configurations
{
    public class ToKhaiYTeBenhNenConfiguration : IEntityTypeConfiguration<ToKhaiYTeBenhNen>
    {
        public void Configure(EntityTypeBuilder<ToKhaiYTeBenhNen> builder)
        {
            builder.ToTable("ToKhaiYTeBenhNens");
            builder.HasKey(x => new { x.ToKhaiYTeId, x.BenhNenId });
            builder.HasOne<ToKhaiYTe>(sc => sc.ToKhaiYTe).WithMany(s => s.ToKhaiYTeBenhNens).HasForeignKey(sc => sc.ToKhaiYTeId);
            builder.HasOne<BenhNen>(sc => sc.BenhNen).WithMany(s => s.ToKhaiYTeBenhNens).HasForeignKey(sc => sc.BenhNenId);
        }
    }
}
