using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces;
using TD.CongDan.Application.DTOs.Identity;
using Microsoft.AspNetCore.Authorization;

namespace TD.CongDan.Api.Controllers.v1
{
    public class RoleController : BaseApiController<RoleController>
    {
        private readonly IUserService userService;
        public RoleController(IUserService service)
        {
            userService = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            var items = await userService.GetRoles(pageNumber, pageSize, keySearch, orderBy);
            return Ok(items);
        }
        /*        public async Task<IActionResult> GetAll()
                {
                    var items = await userService.GetRoles();
                    return Ok(items);
                }*/

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var items = await userService.GetRoleById(id);
            return Ok(items);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> DeleteRoleById(string id)
        {
            var items = await userService.DeleteRoleById(id);
            return Ok(items);
        }


        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> CreateOrUpdateRole(PermissionViewModel role)
        {
            var items = await userService.CreateOrUpdateRole(role);
            return Ok(items);
        }

        
    }
}