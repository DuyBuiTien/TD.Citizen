using AutoMapper;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Application.Features.VehicleTypes.Commands;
using TD.CongDan.Application.Features.VehicleTypes.Queries;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Mappings
{
    internal class VehicleTypeProfile : Profile
    {
        public VehicleTypeProfile()
        {
            CreateMap<CreateVehicleTypeCommand, VehicleType>().ReverseMap();
            CreateMap<VehicleTypesResponse, VehicleType>().ReverseMap();
        }
    }
}