using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.CongDan.Infrastructure.Entities.Company
{
    public class CompanyIndustry : AuditableEntity
    {
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public int? IndustryId { get; set; }
        public Industry Industry { get; set; }
    }
}
