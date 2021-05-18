using System.ComponentModel.DataAnnotations;

namespace TD.CongDan.Application.DTOs.Identity
{
    public class EditUserRequest
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int? GenderId { get; set; }
        public string DateOfBirth { get; set; }
        //Dinh danh cong dan
        public int? IdentityTypeId { get; set; }
        public string IdentityNumber { get; set; }
        public string IdentityPlace { get; set; }
        //ngay cap
        public string IdentityDateOfIssue { get; set; }
        //Quoc tich
        public string Nationality { get; set; }
        //Thuong tru
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? CommuneId { get; set; }
        public string Address { get; set; }
        //tinh trang hon nhan
        public int? MaritalStatusId { get; set; }
    }
}