
using AutoMapper;
using TD.CongDan.Application.Features.Attributes.Commands;
using TD.CongDan.Application.Features.Attributes.Queries;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Application.Mappings
{
    internal class AttributeProfile : Profile
    {
        public AttributeProfile()
        {
            CreateMap<AttributesResponse, Attribute>().ReverseMap();
            CreateMap<AttributeResponse, Attribute>().ReverseMap();
            CreateMap<CreateAttributeCommand, Attribute>().ReverseMap();
        }
    }
}