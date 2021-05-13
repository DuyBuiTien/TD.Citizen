using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class CarpoolConfiguration : IEntityTypeConfiguration<Carpool>
    {
        public void Configure(EntityTypeBuilder<Carpool> builder)
        {
            builder.ToTable("Carpools");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.DepartureDate).HasColumnType("datetime2");
            builder.Property(x => x.DepartureTime).HasColumnType("time");

            builder.HasOne(x => x.VehicleType).WithMany(x => x.Carpools).HasForeignKey(x => x.VehicleTypeId);
            builder.HasOne<Place>(s => s.PlaceDeparture).WithMany(g => g.CarpoolDepartures).HasForeignKey(s => s.PlaceDepartureId);
            builder.HasOne<Place>(s => s.PlaceArrival).WithMany(g => g.CarpoolArrivals).HasForeignKey(s => s.PlaceArrivalId);

        }
    }
}
