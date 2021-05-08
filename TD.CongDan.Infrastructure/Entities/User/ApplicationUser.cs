using Microsoft.AspNetCore.Identity;
using System;

namespace TD.CongDan.Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool IsActive { get; set; } = false;

        public string AvatarUrl { get; set; }

        public int? GenderId { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        //Dinh danh cong dan
        public int? IdentityTypeId { get; set; }
        public IdentityType IdentityType { get; set; }
        public string IdentityNumber { get; set; }
        public string IdentityPlace { get; set; }
        //ngay cap
        public DateTime? IdentityDateOfIssue { get; set; }
        //Quoc tich
        public string Nationality { get; set; }
        //Thuong tru
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? CommuneId { get; set; }
        public string Address { get; set; }
        //tinh trang hon nhan
        public int? MaritalStatusId { get; set; }
        public MaritalStatus MaritalStatus { get; set; }

        //Ton giao
        public int? ReligionId { get; set; }
        public Religion Religion { get; set; }

        public Area Province { get; set; }
        public Area District { get; set; }
        public Area Commune { get; set; }
    }
}