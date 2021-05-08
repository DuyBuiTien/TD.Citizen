using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.CongDan.Domain.Entities.Company
{
    //Phuc loi cong ty
    public class Benefit : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public IList<RecruitmentBenefit> RecruitmentBenefit { get; set; }
    }
}
