using TD.CongDan.Application.DTOs.Identity;
using TD.Libs.Results;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace TD.CongDan.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<ApplicationUserResponse>> GetByUsername(string username);

        Task<PaginatedResult<UserViewModel>> GetAllUsers(int pageNumber, int pageSize, string keySearch, string orderBy);

        Task<PaginatedResult<RoleViewModel>> GetRoles(int pageNumber, int pageSize, string keySearch, string orderBy);

        Task<Result<List<string>>> GetRoleByUsername(string username);



        Task<Result<string>> UpdatePermissionsInRole(PermissionViewModel request);

        Task<Result<string>> EditRoleByUsername(string username, ManageUserRolesViewModel model);

        Task<Result<PermissionViewModel>> GetRoleById(string id);

        Task<Result<string>> DeleteRoleById(string id);

        Task<Result<string>> CreateOrUpdateRole(PermissionViewModel model);

    }
}