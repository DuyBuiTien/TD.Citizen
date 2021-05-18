using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Features.JobApplieds.Queries
{
    public class JobAppliedsResponse
    {
        public string UserName { get; set; }
        public string CVFile { get; set; }

        public int? RecruitmentId { get; set; }
        public Recruitment Recruitment { get; set; }

    }
}