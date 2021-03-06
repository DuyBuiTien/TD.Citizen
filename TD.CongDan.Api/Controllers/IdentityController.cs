using TD.CongDan.Application.DTOs.Identity;
using TD.CongDan.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TD.CongDan.Api.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            this._identityService = identityService;
        }

        /// <summary>
        /// Generates a JSON Web Token , có thể truyền username hoặc email để đăng nhập.
        /// </summary>
        /// <param name="tokenRequest"></param>
        /// <returns></returns>
        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenAsync(TokenRequest tokenRequest)
        {
            var ipAddress = GenerateIPAddress();
            var token = await _identityService.GetTokenAsync(tokenRequest, ipAddress);
            return Ok(token);
        }

        /// <summary>
        /// Đăng ký
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _identityService.RegisterAsync(request, origin));
        }

        /// <summary>
        /// Chỉnh sửa thông tin cá nhân
        /// </summary>
        [HttpPost("edit-infor")]
        public async Task<IActionResult> EditAsync(EditUserRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _identityService.EditAsync(request, origin));
        }

        /// <summary>
        /// Xác thực email
        /// </summary>
        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            return Ok(await _identityService.ConfirmEmailAsync(userId, code));
        }

        /// <summary>
        /// Quên mật khẩu
        /// </summary>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await _identityService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok();
        }

        /// <summary>
        /// Đặt lại mật khẩu
        /// </summary>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            return Ok(await _identityService.ResetPassword(model));
        }

        /// <summary>
        /// Thay đổi ảnh đại diện của cá nhân
        /// </summary>
        [HttpPost("update-avatar"), DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateAvatar([FromForm(Name = "file")] IFormFile file)
        {
            return Ok(await _identityService.UpdateAvatar(file));
        }

        /// <summary>
        /// Get thông tin cá nhân
        /// </summary>
        [HttpGet("user-infor")]
        public async Task<IActionResult> UserInfor()
        {
            return Ok(await _identityService.GetUserInfor());
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}