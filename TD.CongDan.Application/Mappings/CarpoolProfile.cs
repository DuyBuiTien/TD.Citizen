
using AutoMapper;
using TD.CongDan.Application.Features.Benefits.Commands;
using TD.CongDan.Application.Features.Benefits.Queries;
using TD.CongDan.Application.Features.Carpools.Queries;
using TD.CongDan.Application.Features.Degrees.Commands;
using TD.CongDan.Application.Features.Degrees.Queries;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Mappings
{
    internal class CarpoolProfile : Profile
    {
        public CarpoolProfile()
        {
            CreateMap<CarpoolsResponse, Carpool>().ReverseMap();
            CreateMap<CarpoolResponse, Carpool>().ReverseMap();
        }
    }
}