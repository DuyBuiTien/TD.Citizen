
using AutoMapper;
using TD.CongDan.Application.Features.Recruitments.Commands;
using TD.CongDan.Application.Features.Recruitments.Queries;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class RecruitmentProfile : Profile
    {
        public RecruitmentProfile()
        {
            CreateMap<CreateRecruitmentCommand, Recruitment>().ReverseMap();
            CreateMap<RecruitmentResponse, Recruitment>().ReverseMap();
            CreateMap<RecruitmentsResponse, Recruitment>().ReverseMap();

        }
    }
}