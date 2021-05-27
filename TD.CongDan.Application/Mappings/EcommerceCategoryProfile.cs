
using AutoMapper;
using TD.CongDan.Application.Features.EcommerceCategories.Commands;
using TD.CongDan.Application.Features.EcommerceCategories.Queries;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Application.Mappings
{
    internal class EcommerceCategoryProfile : Profile
    {
        public EcommerceCategoryProfile()
        {
            CreateMap<CreateEcommerceCategoryCommand, EcommerceCategory>().ReverseMap();
            CreateMap<EcommerceCategoriesResponse, EcommerceCategory>().ReverseMap();
            CreateMap<EcommerceCategoryResponse, EcommerceCategory>().ReverseMap();
        }
    }
}