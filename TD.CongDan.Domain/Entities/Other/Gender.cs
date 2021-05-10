using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Domain.Entities
{
    //Gioi tinh
    public class Gender: AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<ApplicationUser> UserInfos { get; set; }
        public ICollection<Recruitment> Recruitments { get; set; }


    }
}
