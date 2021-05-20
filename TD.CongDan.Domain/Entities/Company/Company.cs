using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TD.CongDan.Domain.Entities.Company
{
    public class Company : AuditableEntity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string InternationalName { get; set; }
        public string ShortName { get; set; }
        public string TaxCode { get; set; }
        //Dia chi cong ty
        //public string Address { get; set; }
        public int? PlaceId { get; set; }
        public Place Place { get; set; }
        //Dai dien
        public string Representative { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string ProfileVideo { get; set; }
        public string Fax { get; set; }
        //Ngay cap
        public DateTime? DateOfIssue { get; set; }
        //Linh vuc kinh doanh
        public string BusinessSector { get; set; }
        public string Images { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        //Quy mo cong ty
        public string CompanySize { get; set; }


        [JsonIgnore]
        public ICollection<Recruitment> Recruitments { get; set; }
        public ICollection<CompanyIndustry> CompanyIndustries { get; set; }


    }
}
