using AutoMapper;
using TD.CongDan.Application.Features.Categories.Commands;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Application.Features.Categories.Queries.GetAllPaged;
using TD.CongDan.Application.Features.Categories.Queries.GetById;
using TD.CongDan.Application.Features.Products.Queries.GetAllCached;

namespace TD.CongDan.Application.Mappings
{
    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            CreateMap<GetCategoryByIdResponse, Category>().ReverseMap();
            CreateMap<GetAllCategoriesCachedResponse, Category>().ReverseMap();
            CreateMap<JobAgesResponse, Category>().ReverseMap();
        }
    }
}