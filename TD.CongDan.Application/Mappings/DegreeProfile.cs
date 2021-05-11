
using AutoMapper;

using TD.CongDan.Application.Features.Degrees.Commands;
using TD.CongDan.Application.Features.Degrees.Queries;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Mappings
{
    internal class DegreeProfile : Profile
    {
        public DegreeProfile()
        {
            CreateMap<DegreesResponse, Degree>().ReverseMap();
            CreateMap<CreateDegreeCommand, Degree>().ReverseMap();
        }
    }
}