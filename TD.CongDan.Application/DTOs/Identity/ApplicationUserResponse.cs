using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.DTOs.Identity
{
    public class ApplicationUserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }

        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsActive { get; set; } = false;


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
        public Area Province { get; set; }
        public int? DistrictId { get; set; }
        public Area District { get; set; }
        public int? CommuneId { get; set; }
        public Area Commune { get; set; }
        public string Address { get; set; }
        //tinh trang hon nhan
        public int? MaritalStatusId { get; set; }
        public MaritalStatus MaritalStatus { get; set; }

    }
}