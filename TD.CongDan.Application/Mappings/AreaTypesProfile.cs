
using AutoMapper;
using TD.CongDan.Application.Features.AreaTypes.Queries;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Mappings
{
    internal class AreaTypesProfile : Profile
    {
        public AreaTypesProfile()
        {
            CreateMap<AreaTypesResponse, AreaType>().ReverseMap();
        }
    }
}