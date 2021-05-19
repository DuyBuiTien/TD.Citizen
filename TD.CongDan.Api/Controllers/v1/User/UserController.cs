using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces;
using TD.CongDan.Application.DTOs.Identity;
using Microsoft.AspNetCore.Authorization;

namespace TD.CongDan.Api.Controllers.v1
{
    public class UserController : BaseApiController<UserController>
    {
        private readonly IUserService userService;
        public UserController(IUserService service)
        {
            userService = service;
        }

        /// <summary>
        /// Danh sách người dùng, quyền Admin, SuperAdmin được xem
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize,  string keySearch, string orderBy)
        {
            var items = await userService.GetAllUsers(pageNumber, pageSize, keySearch, orderBy);
            return Ok(items);
        }

        /// <summary>
        /// Danh sách Role của người dùng cụ thể
        /// </summary>
        [HttpGet("role/{username}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetRoleByUsername(string username)
        {
            var items = await userService.GetRoleByUsername(username);
            return Ok(items);
        }

        /// <summary>
        /// Chỉnh sửa Role của người dùng cụ thể
        /// </summary>
        [HttpPost("role/{username}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> EditRoleByUsername(string username, ManageUserRolesViewModel model)
        {
            var items = await userService.EditRoleByUsername(username, model);
            return Ok(items);
        }

        /// <summary>
        /// Chi tiết người dùng theo Username
        /// </summary>
        [HttpGet("{username}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var items = await userService.GetByUsername(username);
            return Ok(items);
        }

        /// <summary>
        /// Chỉnh sửa thông tin người dùng
        /// </summary>
        [HttpPost("{username}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> EditByUsername(string username, EditUserRequest request)
        {
            var items = await userService.EditByUsername(username, request);
            return Ok(items);
        }

    }
}