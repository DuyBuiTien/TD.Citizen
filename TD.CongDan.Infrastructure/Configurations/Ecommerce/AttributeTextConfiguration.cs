using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class AttributeTextConfiguration : IEntityTypeConfiguration<AttributeText>
    {
        public void Configure(EntityTypeBuilder<AttributeText> builder)
        {
            builder.ToTable("AttributeTexts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Value).HasColumnType("text");
            builder.HasOne<Attribute>(s => s.Attribute)
            .WithMany(g => g.AttributeTexts)
            .HasForeignKey(s => s.AttributeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
