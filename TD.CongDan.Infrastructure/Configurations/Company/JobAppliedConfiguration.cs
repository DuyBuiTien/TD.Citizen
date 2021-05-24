using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class JobAppliedConfiguration : IEntityTypeConfiguration<JobApplied>
    {
        public void Configure(EntityTypeBuilder<JobApplied> builder)
        {
            builder.ToTable("JobApplieds");
            builder.HasKey(x => new { x.UserName, x.RecruitmentId });

            //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne<Recruitment>(sc => sc.Recruitment).WithMany(s => s.JobApplieds).HasForeignKey(sc => sc.RecruitmentId);

        }
    }
}
