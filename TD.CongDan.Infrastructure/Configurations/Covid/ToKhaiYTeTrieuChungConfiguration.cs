using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Covid;


namespace TD.CongDan.Infrastructure.Configurations
{
    public class ToKhaiYTeTrieuChungConfiguration : IEntityTypeConfiguration<ToKhaiYTeTrieuChung>
    {
        public void Configure(EntityTypeBuilder<ToKhaiYTeTrieuChung> builder)
        {
            builder.ToTable("ToKhaiYTeTrieuChungs");
            builder.HasKey(x => new { x.ToKhaiYTeId, x.TrieuChungId });
            builder.HasOne<ToKhaiYTe>(sc => sc.ToKhaiYTe).WithMany(s => s.ToKhaiYTeTrieuChungs).HasForeignKey(sc => sc.ToKhaiYTeId);
            builder.HasOne<TrieuChung>(sc => sc.TrieuChung).WithMany(s => s.ToKhaiYTeTrieuChungs).HasForeignKey(sc => sc.TrieuChungId);
        }
    }
}
