using System;
using System.Collections.Generic;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Covid
{
    public class NguoiKhaiBao : AuditableEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? GenderId { get; set; }
        public Gender Gender { get; set; }
        //Dinh danh cong dan
        public string IdentityNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        //Quoc tich
        public int NationalityId { get; set; }
        public QuocGia QuocGia { get; set; }
        //Thuong tru
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? CommuneId { get; set; }
        public string Address { get; set; }
       
        public Area Province { get; set; }
        public Area District { get; set; }
        public Area Commune { get; set; }
        public string NguoiNhap { get; set; }


        public ICollection<ToKhaiYTe> ToKhaiYTes { get; set; }

    }
}
