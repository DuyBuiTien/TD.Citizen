using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class EcommerceCategoryProductConfiguration : IEntityTypeConfiguration<EcommerceCategoryProduct>
    {
        public void Configure(EntityTypeBuilder<EcommerceCategoryProduct> builder)
        {
            builder.ToTable("EcommerceCategoryProducts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne<EcommerceCategory>(s => s.EcommerceCategory).WithMany(g => g.EcommerceCategoryProducts).HasForeignKey(s => s.EcommerceCategoryId);
            builder.HasOne<Product>(s => s.Product).WithMany(g => g.EcommerceCategoryProducts).HasForeignKey(s => s.ProductId);

        }
    }
}
