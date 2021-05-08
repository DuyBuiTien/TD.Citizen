using TD.CongDan.Infrastructure.Identity.Models;
using TD.CongDan.Web.Areas.Admin.Models;
using AutoMapper;

namespace TD.CongDan.Web.Areas.Admin.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}