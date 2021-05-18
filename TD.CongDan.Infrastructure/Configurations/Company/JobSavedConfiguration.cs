using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class JobSavedConfiguration : IEntityTypeConfiguration<JobSaved>
    {
        public void Configure(EntityTypeBuilder<JobSaved> builder)
        {
            builder.ToTable("JobSaveds");
            builder.HasKey(x => new { x.UserName, x.RecruitmentId });

            //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne<Recruitment>(sc => sc.Recruitment).WithMany(s => s.JobSaveds).HasForeignKey(sc => sc.RecruitmentId);

        }
    }
}
