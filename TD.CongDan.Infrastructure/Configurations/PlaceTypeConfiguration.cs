using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Infrastructure.Entities;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class PlaceTypeConfiguration : IEntityTypeConfiguration<PlaceType>
    {
        public void Configure(EntityTypeBuilder<PlaceType> builder)
        {
            builder.ToTable("PlaceTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Category).WithMany(x => x.PlaceTypes).HasForeignKey(x => x.CategoryId);
        }
    }
}
