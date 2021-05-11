
using AutoMapper;
using TD.CongDan.Application.Features.Are.Commands;
using TD.CongDan.Application.Features.Are.Queries;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Mappings
{
    internal class AreaProfile : Profile
    {
        public AreaProfile()
        {
            CreateMap<AreasResponse, Area>().ReverseMap();
            CreateMap<CreateAreaCommand, Area>().ReverseMap();
        }
    }
}