using System.Collections.Generic;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Covid
{
    public class ChotKiemDich : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }


        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? CommuneId { get; set; }
        public string Address { get; set; }

        public Area Province { get; set; }
        public Area District { get; set; }
        public Area Commune { get; set; }



        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<ToKhaiYTe> ToKhaiYTes { get; set; }
    }
}
