using System.Collections.Generic;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Features.Companies.Queries
{
    public class CompanyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InternationalName { get; set; }
        public string ShortName { get; set; }
        public string TaxCode { get; set; }
        //Dia chi cong ty
        //public string Address { get; set; }
       /* public string PlaceName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? ProvinceId { get; set; }
        public Area Province { get; set; }
        public int? DistrictId { get; set; }
        public Area District { get; set; }
        public int? CommuneId { get; set; }
        public Area Commune { get; set; }*/

        //Dai dien
        public string Representative { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string ProfileVideo { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        //Ngay cap
        public string DateOfIssueStr { get; set; }
        //Linh vuc kinh doanh
        public string Images { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        //Quy mo cong ty
        public string CompanySize { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; }

        public ICollection<Industry> Industries { get; set; }

    }
}