using TD.Libs.Abstractions.Domain;
using System.Collections.Generic;

namespace TD.CongDan.Domain.Entities
{
    //Tinh trang hon nhan
    public class MaritalStatus: AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<UserInfo> UserInfos { get; set; }
    }
 
}
