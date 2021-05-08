﻿using TD.CongDan.Application.Features.Brands.Commands.Create;
using TD.CongDan.Application.Features.Brands.Commands.Update;
using TD.CongDan.Application.Features.Brands.Queries.GetAllCached;
using TD.CongDan.Application.Features.Brands.Queries.GetById;
using TD.CongDan.Web.Areas.Catalog.Models;
using AutoMapper;

namespace TD.CongDan.Web.Areas.Catalog.Mappings
{
    internal class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<GetAllBrandsCachedResponse, BrandViewModel>().ReverseMap();
            CreateMap<GetBrandByIdResponse, BrandViewModel>().ReverseMap();
            CreateMap<CreateBrandCommand, BrandViewModel>().ReverseMap();
            CreateMap<UpdateBrandCommand, BrandViewModel>().ReverseMap();
        }
    }
}