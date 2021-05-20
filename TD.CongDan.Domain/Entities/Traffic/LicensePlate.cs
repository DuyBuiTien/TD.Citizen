using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Traffic
{
    //Thông tin xe của người dùng
    public class LicensePlate : AuditableEntity
    {
        public string Username { get; set; }
        public string Name { get; set; }
        //Ten chu xe
        public string OwnerFullName { get; set; }
        //Bien so dang ky
        public string LicensePlateNumber { get; set; }
        //Ngay dang ky
        public DateTime? DateOfRegistration { get; set; }
        
        public string Description { get; set; }

       
    }
}
