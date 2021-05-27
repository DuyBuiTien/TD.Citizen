using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class EcommerceCategoryAttributeConfiguration : IEntityTypeConfiguration<EcommerceCategoryAttribute>
    {
        public void Configure(EntityTypeBuilder<EcommerceCategoryAttribute> builder)
        {
            builder.ToTable("EcommerceCategoryAttributes");
            builder.HasKey(x => new { x.EcommerceCategoryId, x.AttributeId });
            builder.HasOne<EcommerceCategory>(s => s.EcommerceCategory).WithMany(g => g.EcommerceCategoryAttributes).HasForeignKey(s => s.EcommerceCategoryId);
            builder.HasOne<Attribute>(s => s.Attribute).WithMany(g => g.EcommerceCategoryAttributes).HasForeignKey(s => s.AttributeId);

        }
    }
}
