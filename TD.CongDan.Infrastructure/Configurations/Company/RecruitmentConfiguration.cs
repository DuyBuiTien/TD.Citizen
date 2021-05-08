using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Infrastructure.Entities.Company;


namespace TD.CongDan.Infrastructure.Configurations
{
    public class RecruitmentConfiguration : IEntityTypeConfiguration<Recruitment>
    {
        public void Configure(EntityTypeBuilder<Recruitment> builder)
        {
            builder.ToTable("Recruitments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();

            builder.HasOne(x => x.Company).WithMany(x => x.Recruitments).HasForeignKey(x => x.CompanyId);
            builder.HasOne(x => x.Degree).WithMany(x => x.Recruitments).HasForeignKey(x => x.DegreeId);
            builder.HasOne(x => x.JobPosition).WithMany(x => x.Recruitments).HasForeignKey(x => x.JobPositionId);
            builder.HasOne(x => x.JobType).WithMany(x => x.Recruitments).HasForeignKey(x => x.JobTypeId);
            builder.HasOne(x => x.JobName).WithMany(x => x.Recruitments).HasForeignKey(x => x.JobNameId);
            builder.HasOne(x => x.JobAge).WithMany(x => x.Recruitments).HasForeignKey(x => x.JobAgeId);
            builder.HasOne(x => x.Experience).WithMany(x => x.Recruitments).HasForeignKey(x => x.ExperienceId);
            builder.HasOne(x => x.Salary).WithMany(x => x.Recruitments).HasForeignKey(x => x.SalaryId);
            builder.HasOne(x => x.Gender).WithMany(x => x.Recruitments).HasForeignKey(x => x.GenderId);
        }
    }
}
