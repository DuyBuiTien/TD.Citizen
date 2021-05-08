using TD.CongDan.Application.Features.Products.Commands.Create;
using TD.CongDan.Application.Features.Products.Commands.Update;
using TD.CongDan.Application.Features.Products.Queries.GetAllCached;
using TD.CongDan.Application.Features.Products.Queries.GetById;
using TD.CongDan.Web.Areas.Catalog.Models;
using AutoMapper;

namespace TD.CongDan.Web.Areas.Catalog.Mappings
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetAllProductsCachedResponse, ProductViewModel>().ReverseMap();
            CreateMap<GetProductByIdResponse, ProductViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, ProductViewModel>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductViewModel>().ReverseMap();
        }
    }
}