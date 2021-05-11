
using AutoMapper;
using TD.CongDan.Application.Features.Benefits.Commands;
using TD.CongDan.Application.Features.Benefits.Queries;
using TD.CongDan.Application.Features.Degrees.Commands;
using TD.CongDan.Application.Features.Degrees.Queries;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class BenefitProfile : Profile
    {
        public BenefitProfile()
        {
            CreateMap<BenefitsResponse, Benefit>().ReverseMap();
            CreateMap<CreateBenefitCommand, Degree>().ReverseMap();
        }
    }
}