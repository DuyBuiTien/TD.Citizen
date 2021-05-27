using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class AttributeDecimalConfiguration : IEntityTypeConfiguration<AttributeDecimal>
    {
        public void Configure(EntityTypeBuilder<AttributeDecimal> builder)
        {
            builder.ToTable("AttributeDecimals");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne<Product>(s => s.Product)
            .WithMany(g => g.AttributeDecimals)
            .HasForeignKey(s => s.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<Attribute>(s => s.Attribute)
            .WithMany(g => g.AttributeDecimals)
            .HasForeignKey(s => s.AttributeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
