
using AutoMapper;
using TD.CongDan.Application.Features.Experiences.Commands;
using TD.CongDan.Application.Features.Experiences.Queries;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class ExperienceProfile : Profile
    {
        public ExperienceProfile()
        {
            CreateMap<ExperiencesResponse, Experience>().ReverseMap();
            CreateMap<CreateExperienceCommand, Experience>().ReverseMap();
        }
    }
}