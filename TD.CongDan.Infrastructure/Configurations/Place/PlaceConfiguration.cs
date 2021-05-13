using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.ToTable("Places");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Content).HasMaxLength(500);
            builder.Property(x => x.Latitude).HasColumnType("Decimal(8,6)");
            builder.Property(x => x.Longitude).HasColumnType("Decimal(9,6)");
            builder.Property(x => x.ContentHtml).HasColumnType("text");
            //builder.Property(x =>x.Location).HasColumnType("geometry");


            builder.HasOne(x => x.PlaceType).WithMany(x => x.Places).HasForeignKey(x => x.PlaceTypeId);
            builder.HasOne<Area>(s => s.Province).WithMany(g => g.PlaceProvinces).HasForeignKey(s => s.ProvinceId);
            builder.HasOne<Area>(s => s.District).WithMany(g => g.PlaceDistricts).HasForeignKey(s => s.DistrictId);
            builder.HasOne<Area>(s => s.Commune).WithMany(g => g.PlaceCommunes).HasForeignKey(s => s.CommuneId);

           // builder.HasMany<Carpool>(s => s.CarpoolArrivals).WithOne(g => g.PlaceArrival).HasForeignKey(s => s.PlaceArrivalId);

        }
    }
}
