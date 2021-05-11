using AutoMapper;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Application.Features.PlaceTypes.Commands;
using TD.CongDan.Application.Features.PlaceTypes.Queries;

namespace TD.CongDan.Application.Mappings
{
    internal class PlaceTypeProfile : Profile
    {
        public PlaceTypeProfile()
        {
            CreateMap<CreatePlaceTypeCommand, PlaceType>().ReverseMap();
            CreateMap<PlaceTypesResponse, PlaceType>().ReverseMap();
        }
    }
}