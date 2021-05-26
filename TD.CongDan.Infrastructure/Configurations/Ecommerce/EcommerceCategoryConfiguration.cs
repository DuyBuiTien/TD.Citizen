using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class EcommerceCategoryConfiguration : IEntityTypeConfiguration<EcommerceCategory>
    {
        public void Configure(EntityTypeBuilder<EcommerceCategory> builder)
        {
            builder.ToTable("EcommerceCategories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(e => e.Tags).HasConversion(v => string.Join(',', v),v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
