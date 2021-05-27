using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class AttributeVarcharConfiguration : IEntityTypeConfiguration<AttributeVarchar>
    {
        public void Configure(EntityTypeBuilder<AttributeVarchar> builder)
        {
            builder.ToTable("AttributeVarchars");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne<Product>(s => s.Product)
            .WithMany(g => g.AttributeVarchars)
            .HasForeignKey(s => s.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<Attribute>(s => s.Attribute)
            .WithMany(g => g.AttributeVarchars)
            .HasForeignKey(s => s.AttributeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
