
using AutoMapper;
using TD.CongDan.Application.Features.Religions.Queries;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Mappings
{
    internal class ReligionProfile : Profile
    {
        public ReligionProfile()
        {
            CreateMap<ReligionsResponse, Religion>().ReverseMap();
        }
    }
}