using TD.CongDan.Application.Features.Products.Commands.Create;
using TD.CongDan.Application.Features.Products.Queries.GetAllCached;
using TD.CongDan.Application.Features.Products.Queries.GetAllPaged;
using TD.CongDan.Application.Features.Products.Queries.GetById;
using TD.CongDan.Domain.Entities.Catalog;
using AutoMapper;

namespace TD.CongDan.Application.Mappings
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<GetProductByIdResponse, Product>().ReverseMap();
            CreateMap<GetAllProductsCachedResponse, Product>().ReverseMap();
            CreateMap<GetAllProductsResponse, Product>().ReverseMap();
        }
    }
}