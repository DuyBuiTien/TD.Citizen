
using AutoMapper;
using TD.CongDan.Application.Features.Benefits.Commands;
using TD.CongDan.Application.Features.Benefits.Queries;
using TD.CongDan.Application.Features.Companies.Commands;
using TD.CongDan.Application.Features.Companies.Queries;
using TD.CongDan.Application.Features.Degrees.Commands;
using TD.CongDan.Application.Features.Degrees.Queries;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompaniesResponse, Company>().ReverseMap();
            CreateMap<CompanyResponse, Company>().ReverseMap();

            CreateMap<CreateCompanyCommand, Company>().ReverseMap();
        }
    }
}