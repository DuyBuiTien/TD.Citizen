using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TD.CongDan.Application.DTOs.Identity;

namespace TD.CongDan.Infrastructure.Identity.Services
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ReverseMap();
        }
    }
}