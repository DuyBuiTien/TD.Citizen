using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class TrafficTicketConfiguration : IEntityTypeConfiguration<TrafficTicket>
    {
        public void Configure(EntityTypeBuilder<TrafficTicket> builder)
        {
            builder.ToTable("TrafficTickets");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
          

        }
    }
}
