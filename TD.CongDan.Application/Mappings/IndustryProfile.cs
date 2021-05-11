
using AutoMapper;
using TD.CongDan.Application.Features.Industries.Commands;
using TD.CongDan.Application.Features.Industries.Queries;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class IndustryProfile : Profile
    {
        public IndustryProfile()
        {
            CreateMap<IndustriesResponse, Industry>().ReverseMap();
            CreateMap<CreateIndustryCommand, Industry>().ReverseMap();
        }
    }
}