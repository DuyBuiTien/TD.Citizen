using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TD.CongDan.Domain.Entities.Company
{
    //Phuc loi - Tuyen dung
    public class RecruitmentBenefit : AuditableEntity
    {
        public string Name { get; set; }
        
        public int RecruitmentId { get; set; }
        [JsonIgnore]
        public Recruitment Recruitment { get; set; }

        public int BenefitId { get; set; }
        public Benefit Benefit { get; set; }
    }
}
