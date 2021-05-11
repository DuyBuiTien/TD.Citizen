
using AutoMapper;
using TD.CongDan.Application.Features.Genders.Queries;
using TD.CongDan.Application.Features.Salaries.Commands;
using TD.CongDan.Application.Features.Salaries.Queries;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class GenderProfile : Profile
    {
        public GenderProfile()
        {
            CreateMap<GendersResponse, Gender>().ReverseMap();
        }
    }
}