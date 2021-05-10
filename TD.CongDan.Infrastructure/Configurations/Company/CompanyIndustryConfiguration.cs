using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class CompanyIndustryConfiguration : IEntityTypeConfiguration<CompanyIndustry>
    {
        public void Configure(EntityTypeBuilder<CompanyIndustry> builder)
        {
            builder.ToTable("CompanyIndustries");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne<Company>(sc => sc.Company).WithMany(s => s.CompanyIndustries).HasForeignKey(sc => sc.CompanyId);
            builder.HasOne<Industry>(sc => sc.Industry).WithMany(s => s.CompanyIndustries).HasForeignKey(sc => sc.IndustryId);
        }
    }
}
