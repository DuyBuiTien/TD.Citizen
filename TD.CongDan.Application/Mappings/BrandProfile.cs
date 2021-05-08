using TD.CongDan.Application.Features.Brands.Commands.Create;
using TD.CongDan.Application.Features.Brands.Queries.GetAllCached;
using TD.CongDan.Application.Features.Brands.Queries.GetById;
using TD.CongDan.Domain.Entities.Catalog;
using AutoMapper;

namespace TD.CongDan.Application.Mappings
{
    internal class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<CreateBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsCachedResponse, Brand>().ReverseMap();
        }
    }
}