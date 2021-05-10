using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
    {
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            builder.ToTable("JobApplications");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();

            builder.HasOne(x => x.Position).WithMany(x => x.JobApplications).HasForeignKey(x => x.PositionId);
            builder.HasOne(x => x.CurrentPosition).WithMany(x => x.CurrentJobApplications).HasForeignKey(x => x.CurrentPositionId);
            builder.HasOne(x => x.Experience).WithMany(x => x.JobApplications).HasForeignKey(x => x.ExperienceId);
            builder.HasOne(x => x.Degree).WithMany(x => x.JobApplications).HasForeignKey(x => x.DegreeId);
            builder.HasOne(x => x.JobType).WithMany(x => x.JobApplications).HasForeignKey(x => x.JobTypeId);
        }
    }
}
