using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class AttributeDatetimeConfiguration : IEntityTypeConfiguration<AttributeDatetime>
    {
        public void Configure(EntityTypeBuilder<AttributeDatetime> builder)
        {
            builder.ToTable("AttributeDatetimes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Value).HasColumnType("datetime2");
            builder.HasOne<Product>(s => s.Product)
            .WithMany(g => g.AttributeDatetimes)
            .HasForeignKey(s => s.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<Attribute>(s => s.Attribute)
            .WithMany(g => g.AttributeDatetimes)
            .HasForeignKey(s => s.AttributeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
