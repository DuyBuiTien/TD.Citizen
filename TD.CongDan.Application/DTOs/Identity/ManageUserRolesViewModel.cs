using System.Collections.Generic;

namespace TD.CongDan.Application.DTOs.Identity
{
    public class ManageUserRolesViewModel
    {
        public string UserName { get; set; }
        public IList<UserRolesViewModel> UserRoles { get; set; }
    }

    public class UserRolesViewModel
    {
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}