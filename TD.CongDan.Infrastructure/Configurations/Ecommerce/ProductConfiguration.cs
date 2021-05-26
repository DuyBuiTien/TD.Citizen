using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x=>x.Description).HasColumnType("text");
            builder.HasOne<Brand>(s => s.Brand).WithMany(g => g.Products).HasForeignKey(s => s.BrandId);
            builder.HasOne<EcommerceCategory>(s => s.PrimaryEcommerceCategory).WithMany(g => g.PrimaryProducts).HasForeignKey(s => s.PrimaryEcommerceCategoryId);
        }
    }
}
