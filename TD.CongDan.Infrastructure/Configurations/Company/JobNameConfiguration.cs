using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Infrastructure.Entities.Company;


namespace TD.CongDan.Infrastructure.Configurations
{
    public class JobNameConfiguration : IEntityTypeConfiguration<JobName>
    {
        public void Configure(EntityTypeBuilder<JobName> builder)
        {
            builder.ToTable("JobNames");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();

        }
    }
}
