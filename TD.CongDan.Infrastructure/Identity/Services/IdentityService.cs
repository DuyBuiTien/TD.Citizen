using TD.CongDan.Application.DTOs.Identity;
using TD.CongDan.Application.DTOs.Mail;
using TD.CongDan.Application.DTOs.Settings;
using TD.CongDan.Application.Enums;
using TD.CongDan.Application.Exceptions;
using TD.CongDan.Application.Interfaces;
using TD.CongDan.Application.Interfaces.Shared;
using TD.Libs.Results;
using TD.Libs.ThrowR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.IO;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Contexts;

namespace TD.CongDan.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMailService _mailService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IGenderRepository _repositoryAsync;
        private readonly IApplicationDbContext _applicationDbContext;
        //private readonly IGender gender;


        public IdentityService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            IApplicationDbContext applicationDbContext,
            SignInManager<ApplicationUser> signInManager, IMailService mailService, IAuthenticatedUserService authenticatedUserService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            _mailService = mailService;
            _authenticatedUserService = authenticatedUserService;
            _applicationDbContext = applicationDbContext;

        }

        public async Task<Result<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress)
        {

            ApplicationUser user;
            if (string.IsNullOrWhiteSpace(request.UserName))
            {
                user = await _userManager.FindByEmailAsync(request.Email);
            } else
            {
                user = await _userManager.FindByNameAsync(request.UserName);
            }
             
            Throw.Exception.IfNull(user, nameof(user), $"No Accounts Registered with {request.Email}.");
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            Throw.Exception.IfFalse(user.EmailConfirmed, $"Email is not confirmed for '{request.Email}'.");
            Throw.Exception.IfFalse(user.IsActive, $"Account for '{request.Email}' is not active. Please contact the Administrator.");
            Throw.Exception.IfFalse(result.Succeeded, $"Invalid Credentials for '{request.Email}'.");
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user, ipAddress);
            var response = new TokenResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.IssuedOn = jwtSecurityToken.ValidFrom.ToLocalTime();
            response.ExpiresOn = jwtSecurityToken.ValidTo.ToLocalTime();
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return Result<TokenResponse>.Success(response, "Authenticated");
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user, string ipAddress)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("first_name", user.FirstName),
                new Claim("last_name", user.LastName),
                new Claim("full_name", $"{user.FirstName} {user.LastName}"),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);
            return JWTGeneration(claims);
        }

        private JwtSecurityToken JWTGeneration(IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        public async Task<Result<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                throw new ApiException($"Username '{request.UserName}' is already taken.");
            }


            CultureInfo provider = CultureInfo.InvariantCulture;

            DateTime? dateOfBirth = null;
            try { dateOfBirth = DateTime.ParseExact(request.DateOfBirth, "dd/MM/yyyy", provider); } catch { }
            DateTime? identityDateOfIssue = null;
            try { identityDateOfIssue = DateTime.ParseExact(request.IdentityDateOfIssue, "dd/MM/yyyy", provider); } catch { }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                GenderId = request.GenderId,
                DateOfBirth = dateOfBirth,
                IdentityTypeId = request.IdentityTypeId,
                IdentityNumber = request.IdentityNumber,
                IdentityPlace = request.IdentityPlace,
                IdentityDateOfIssue = identityDateOfIssue,
                Nationality = request.Nationality,
                ProvinceId = request.ProvinceId,
                DistrictId = request.DistrictId,
                CommuneId = request.CommuneId,
                Address = request.Address,
                MaritalStatusId = request.MaritalStatusId,

            };
            //var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            var userWithSameEmail = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    var verificationUri = await SendVerificationEmail(user, origin);
                    //TODO: Attach Email Service here and configure it via appsettings
                    await _mailService.SendAsync(new MailRequest() { From = "mail@codewithmukesh.com", To = user.Email, Body = $"Please confirm your account by <a href='{verificationUri}'>clicking here</a>.", Subject = "Confirm Registration" });
                    return Result<string>.Success(user.Id, message: $"User Registered. Confirmation Mail has been delivered to your Mailbox. (DEV) Please confirm your account by visiting this URL {verificationUri}");
                }
                else
                {
                    throw new ApiException($"{result.Errors}");
                }
            }
            else
            {
                throw new ApiException($"UserName {request.UserName } is already registered.");
            }
        }

        private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/identity/confirm-email/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;
        }

        public async Task<Result<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Result<string>.Success(user.Id, message: $"Account Confirmed for {user.Email}. You can now use the /api/identity/token endpoint to generate JWT.");
            }
            else
            {
                throw new ApiException($"An error occured while confirming {user.Email}.");
            }
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) return;

            var code = await _userManager.GeneratePasswordResetTokenAsync(account);
            var route = "api/identity/reset-password/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var emailRequest = new MailRequest()
            {
                Body = $"You reset token is - {code}",
                To = model.Email,
                Subject = "Reset Password",
            };
            //await _mailService.SendAsync(emailRequest);
        }

        public async Task<Result<string>> ResetPassword(ResetPasswordRequest model)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);
            if (account == null) throw new ApiException($"No Accounts Registered with {model.Email}.");
            var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
            if (result.Succeeded)
            {
                return Result<string>.Success(model.Email, message: $"Password Resetted.");
            }
            else
            {
                throw new ApiException($"Error occured while reseting the password.");
            }
        }

        public async Task<Result<string>> UpdateAvatar(IFormFile file)
        {
            var id = _authenticatedUserService.UserId;
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) throw new ApiException("Không tìm thấy tài khoản!");

            var fileName = file.FileName;
            var folderName = Path.Combine("Resources", "Files");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            Guid dir_UUID = Guid.NewGuid();
            string dir_UUID_String = dir_UUID.ToString();

            var target = Path.Combine(pathToSave, dir_UUID_String);
            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }

            var fullPath = Path.Combine(target, fileName);
            var dbPath = Path.Combine(folderName, dir_UUID_String, fileName);

            using (var stream = System.IO.File.Create(fullPath))
            {
                await file.CopyToAsync(stream);
                user.AvatarUrl = dbPath.Replace("\\", "/");
            }

            await _userManager.UpdateAsync(user);
            return Result<string>.Success(dbPath.Replace("\\", "/"));
        }

        public async Task<Result<ApplicationUserResponse>> GetUserInfor()
        {
            var id = _authenticatedUserService.UserId;

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new ApiException("Không tìm thấy tài khoản!");

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);


            //var gender = await _repositoryAsync.GetByIdAsync(1);
            var gender = await _applicationDbContext.Genders.FindAsync(user.GenderId);
            var identityType = await _applicationDbContext.IdentityTypes.FindAsync(user.IdentityTypeId);
            var Province = await _applicationDbContext.Areas.FindAsync(user.ProvinceId);
            var District = await _applicationDbContext.Areas.FindAsync(user.DistrictId);
            var Commune = await _applicationDbContext.Areas.FindAsync(user.CommuneId);
            var MaritalStatus = await _applicationDbContext.MaritalStatuses.FindAsync(user.MaritalStatusId);

            return Result<ApplicationUserResponse>.Success(new ApplicationUserResponse()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                GenderId = user.GenderId,
                Gender = gender,
                DateOfBirth = user.DateOfBirth,
                IdentityTypeId = user.IdentityTypeId,
                IdentityType = identityType,
                IdentityNumber = user.IdentityNumber,
                IdentityPlace = user.IdentityPlace,
                IdentityDateOfIssue = user.IdentityDateOfIssue,
                Nationality = user.Nationality,
                ProvinceId = user.ProvinceId,
                Province = Province,
                DistrictId = user.DistrictId,
                District = District,
                CommuneId = user.CommuneId,
                Commune = Commune,
                Address = user.Address,
                MaritalStatusId = user.MaritalStatusId,
                MaritalStatus = MaritalStatus,
                IsActive = user.IsActive,
                IsVerified = user.IsActive,
                Roles = rolesList.ToList(),
            });
        }

        public async Task<Result<ApplicationUserResponse>> UpdateUserInfor(ApplicationUserEditRequest command)
        {
            var id = _authenticatedUserService.UserId;
            var user = await _userManager.FindByIdAsync(id);

            if (user == null ) throw new ApiException("Không tìm thấy tài khoản!");
            CultureInfo provider = CultureInfo.InvariantCulture;

            DateTime? dateOfBirth = user.DateOfBirth;
            try { dateOfBirth = DateTime.ParseExact(command.DateOfBirth, "dd/MM/yyyy", provider); } catch { }
            DateTime? identityDateOfIssue = user.IdentityDateOfIssue;
            try { identityDateOfIssue = DateTime.ParseExact(command.IdentityDateOfIssue, "dd/MM/yyyy", provider); } catch { }


            user.FirstName = command.FirstName ?? user.FirstName;
            user.LastName = command.LastName ?? user.LastName;
            user.GenderId = command.GenderId ?? user.GenderId;
            user.IdentityTypeId = command.IdentityTypeId ?? user.IdentityTypeId;
            user.IdentityNumber = command.IdentityNumber ?? user.IdentityNumber;
            user.IdentityPlace = command.IdentityPlace ?? user.IdentityPlace;
            user.Nationality = command.Nationality ?? user.Nationality;
            user.ProvinceId = command.ProvinceId ?? user.ProvinceId;
            user.DistrictId = command.DistrictId ?? user.DistrictId;
            user.CommuneId = command.CommuneId ?? user.CommuneId;
            user.Address = command.Address ?? user.Address;
            user.MaritalStatusId = command.MaritalStatusId ?? user.MaritalStatusId;
            user.DateOfBirth = dateOfBirth;
            user.IdentityDateOfIssue = identityDateOfIssue;

            await _userManager.UpdateAsync(user);

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);


            return Result<ApplicationUserResponse>.Success(new ApplicationUserResponse()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                GenderId = user.GenderId,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                IdentityTypeId = user.IdentityTypeId,
                IdentityType = user.IdentityType,
                IdentityNumber = user.IdentityNumber,
                IdentityPlace = user.IdentityPlace,
                IdentityDateOfIssue = user.IdentityDateOfIssue,
                Nationality = user.Nationality,
                ProvinceId = user.ProvinceId,
                Province = user.Province,
                DistrictId = user.DistrictId,
                District = user.District,
                CommuneId = user.CommuneId,
                Commune = user.Commune,
                Address = user.Address,
                MaritalStatus = user.MaritalStatus,
                IsActive = user.IsActive,
                IsVerified = user.IsActive,
                Roles = rolesList.ToList(),
                AvatarUrl = user.AvatarUrl,

            });
        }
    }
}