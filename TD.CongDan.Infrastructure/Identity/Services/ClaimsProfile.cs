using AutoMapper;
using System.Security.Claims;
using TD.CongDan.Application.DTOs.Identity;

namespace TD.CongDan.Web.Areas.Admin.Mappings
{
    public class ClaimsProfile : Profile
    {
        public ClaimsProfile()
        {
            CreateMap<Claim, RoleClaimsViewModel>().ReverseMap();
        }
    }
}