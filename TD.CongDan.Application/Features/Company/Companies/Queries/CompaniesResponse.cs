using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.Companies.Queries
{
    public class CompaniesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InternationalName { get; set; }
        public string ShortName { get; set; }
        public string TaxCode { get; set; }
        //Dia chi cong ty
        //public string Address { get; set; }
        public string PlaceName { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Commune { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        //Quy mo cong ty
        public string CompanySize { get; set; }

    }
}