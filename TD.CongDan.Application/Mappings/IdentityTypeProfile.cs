
using AutoMapper;
using TD.CongDan.Application.Features.IdentityTypes.Queries;

using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Mappings
{
    internal class IdentityTypeProfile : Profile
    {
        public IdentityTypeProfile()
        {
            CreateMap<IdentityTypesResponse, IdentityType>().ReverseMap();
        }
    }
}