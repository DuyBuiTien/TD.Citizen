
using AutoMapper;

using TD.CongDan.Application.Features.AttributeValues.Commands;
using TD.CongDan.Application.Features.AttributeValues.Queries;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Application.Mappings
{
    internal class AttributeValueProfile : Profile
    {
        public AttributeValueProfile()
        {
            CreateMap<AttributevaluesResponse, AttributeValue>().ReverseMap();
            CreateMap<AttributeValueResponse, AttributeValue>().ReverseMap();
            CreateMap<CreateAttributeValueCommand, AttributeValue>().ReverseMap();
        }
    }
}