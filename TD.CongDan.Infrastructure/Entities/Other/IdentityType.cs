using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.CongDan.Infrastructure.Entities
{
    //Loai giay to
    public class IdentityType:  AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ICollection<ApplicationUser> UserInfos { get; set; }
    }
}
