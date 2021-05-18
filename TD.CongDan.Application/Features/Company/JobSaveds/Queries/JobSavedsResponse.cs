using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Features.JobSaveds.Queries
{
    public class JobSavedsResponse
    {
        public string UserName { get; set; }
        public int? RecruitmentId { get; set; }
        public Recruitment Recruitment { get; set; }

    }
}