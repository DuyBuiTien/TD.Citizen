
using AutoMapper;
using TD.CongDan.Application.Features.Salaries.Commands;
using TD.CongDan.Application.Features.Salaries.Queries;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class SalaryProfile : Profile
    {
        public SalaryProfile()
        {
            CreateMap<SalariesResponse, Salary>().ReverseMap();
            CreateMap<CreateSalaryCommand, Salary>().ReverseMap();
        }
    }
}