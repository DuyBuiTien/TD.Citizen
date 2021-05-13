using TD.CongDan.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces;
using TD.CongDan.Application.DTOs.Identity;

namespace TD.CongDan.Api.Controllers.v1
{
    public class UserController : BaseApiController<UserController>
    {
        private readonly IUserService userService;
        public UserController(IUserService service)
        {
            userService = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize,  string keySearch, string orderBy)
        {
            var items = await userService.GetAllUsers(pageNumber, pageSize, keySearch, orderBy);
            return Ok(items);
        }


        [HttpGet("role/{username}")]
        public async Task<IActionResult> GetRoleByUsername(string username)
        {
            var items = await userService.GetRoleByUsername(username);
            return Ok(items);
        }

        [HttpPost("role/{username}")]
        public async Task<IActionResult> EditRoleByUsername(string username, ManageUserRolesViewModel model)
        {
            var items = await userService.EditRoleByUsername(username, model);
            return Ok(items);
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var items = await userService.GetByUsername(username);
            return Ok(items);
        }

    }
}