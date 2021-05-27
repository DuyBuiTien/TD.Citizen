using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;
using System.Text.Json.Serialization;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Domain.Entities
{
    //Gioi tinh
    public class Gender: AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<ApplicationUser> UserInfos { get; set; }
        [JsonIgnore]
        public ICollection<Recruitment> Recruitments { get; set; }

        [JsonIgnore]
        public ICollection<NguoiKhaiBao> NguoiKhaiBaos { get; set; }
    }
}
