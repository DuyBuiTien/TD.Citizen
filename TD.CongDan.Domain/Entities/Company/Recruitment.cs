using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TD.CongDan.Domain.Entities.Company
{
    //Tuyen dung
    public class Recruitment : AuditableEntity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        //Cong ty
        public int? CompanyId { get; set; }
        //Loai hinh cong viec
        public int? JobTypeId { get; set; }
        //Nghe nghiep
        public int? JobNameId { get; set; }
        //Vi tri
        public int? JobPositionId { get; set; }
        //Muc luong
        public int? SalaryId { get; set; }

        //Kinh nghiem
        public int? ExperienceId { get; set; }
        //Yeu cau gioi tinh
        public int? GenderId { get; set; }
        
        public int? JobAgeId { get; set; }
        public int? DegreeId { get; set; }

        //yeu cau khac
        public string OtherRequirement {get;set;}
        //ho so bao gom
        public string ResumeRequirement { get; set; }

        public DateTime? ResumeApplyExpired { get; set; }
        //So luong
        public int NumberOfJob { get; set; }
        public int Status { get; set; }

        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactAdress { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual Company Company { get; set; }
        public virtual JobPosition JobPosition { get; set; }
        public virtual JobType JobType { get; set; }
        public virtual JobName JobName { get; set; }
        public virtual Salary Salary { get; set; }
        public virtual JobAge JobAge { get; set; }
        public virtual Degree Degree { get; set; }
        public virtual Experience Experience { get; set; }

        /*[JsonIgnore]*/
        public virtual ICollection<RecruitmentBenefit> RecruitmentBenefit { get; set; }
        [JsonIgnore]
        public virtual ICollection<JobSaved> JobSaveds { get; set; }
        [JsonIgnore]
        public virtual ICollection<JobApplied> JobApplieds { get; set; }
        

        public int? PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}
