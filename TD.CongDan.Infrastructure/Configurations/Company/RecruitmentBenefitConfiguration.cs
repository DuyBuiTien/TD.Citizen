using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Configurations
{
    public class RecruitmentBenefitConfiguration : IEntityTypeConfiguration<RecruitmentBenefit>
    {
        public void Configure(EntityTypeBuilder<RecruitmentBenefit> builder)
        {
            builder.ToTable("RecruitmentBenefits");
            //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasKey(x => new { x.BenefitId, x.RecruitmentId });
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.HasOne<Benefit>(sc => sc.Benefit).WithMany(s => s.RecruitmentBenefit).HasForeignKey(sc => sc.BenefitId);
            builder.HasOne<Recruitment>(sc => sc.Recruitment).WithMany(s => s.RecruitmentBenefit).HasForeignKey(sc => sc.RecruitmentId);
        }
    }
}
