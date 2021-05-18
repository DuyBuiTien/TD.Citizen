using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Company
{
    //Việc đã lưu
    public class JobSaved : AuditableEntity
    {
        public string UserName { get; set; }
        public int? RecruitmentId { get; set; }
        public Recruitment Recruitment { get; set; }
    }
}
