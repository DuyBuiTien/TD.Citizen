using TD.CongDan.Application.DTOs.Identity;
using TD.Libs.Results;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TD.CongDan.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);

        Task<Result<string>> RegisterAsync(RegisterRequest request, string origin);

        Task<Result<string>> EditAsync(EditUserRequest request, string origin);



        Task<Result<string>> ConfirmEmailAsync(string userId, string code);

        Task ForgotPassword(ForgotPasswordRequest model, string origin);

        Task<Result<string>> ResetPassword(ResetPasswordRequest model);

        Task<Result<string>> UpdateAvatar(IFormFile file);
        Task<Result<ApplicationUserResponse>> GetUserInfor();
        Task<Result<ApplicationUserResponse>> UpdateUserInfor(ApplicationUserEditRequest model);

    }
}