using System.Collections.Generic;

namespace TD.CongDan.Application.DTOs.Identity
{
    public class PermissionViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<RoleClaimsViewModel> RoleClaims { get; set; }
    }

    public class RoleClaimsViewModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }
}