
using AutoMapper;
using TD.CongDan.Application.Features.Genders.Queries;
using TD.CongDan.Application.Features.LicensePlates.Commands;
using TD.CongDan.Application.Features.LicensePlates.Queries;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Mappings
{
    internal class LicensePlateProfile : Profile
    {
        public LicensePlateProfile()
        {
            CreateMap<LicensePlatesResponse, LicensePlate>().ReverseMap();
            CreateMap<CreateLicensePlateCommand, LicensePlate>().ReverseMap();
        }
    }
}