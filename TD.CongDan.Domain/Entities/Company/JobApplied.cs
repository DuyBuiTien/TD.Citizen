using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.CongDan.Domain.Entities.Company
{
    //Viec da ứng tuyển
    public class JobApplied : AuditableEntity
    {
        public string UserName { get; set; }
        public string CVFile { get; set; }
        public int? RecruitmentId { get; set; }
        public Recruitment Recruitment { get; set; }
    }
}
