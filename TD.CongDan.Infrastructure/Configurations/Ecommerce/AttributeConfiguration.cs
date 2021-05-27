using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Ecommerce;
using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class AttributeConfiguration : IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {
            builder.ToTable("Attributes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Code).IsRequired();
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x => x.IsVisibleOnFront).HasDefaultValue(true);
            builder.Property(x => x.IsRequired).HasDefaultValue(true);
            builder.Property(x => x.IsEditable).HasDefaultValue(true);
            builder.Property(x => x.IsSellerEditable).HasDefaultValue(true);
            builder.Property(e => e.FrontendInput).HasConversion(v => v.ToString(),v => (FrontendInput)System.Enum.Parse(typeof(FrontendInput), v));
            builder.Property(e => e.InputType).HasConversion(v => v.ToString(), v => (FrontendInput)System.Enum.Parse(typeof(FrontendInput), v));
            builder.Property(e => e.DataType).HasConversion(v => v.ToString(), v => (DataType)System.Enum.Parse(typeof(FrontendInput), v));
        }
    }
}
