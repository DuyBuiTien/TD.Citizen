using AutoMapper;
using TD.CongDan.Application.Features.Places.Commands;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Application.Features.Places.Queries;

namespace TD.CongDan.Application.Mappings
{
    internal class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<CreatePlaceCommand, Place>().ReverseMap();
            CreateMap<PlaceResponse, Place>().ReverseMap();
        }
    }
}