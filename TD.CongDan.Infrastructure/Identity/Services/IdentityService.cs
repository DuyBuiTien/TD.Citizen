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
            //Throw.Exception.IfFalse(user.IsActive, $"Account for '{request.Email}' is not active. Please contact the Administrator.");
            Throw.Exception.IfFalse(result.Succeeded, $"Invalid Credentials for '{request.Email}'.");
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user, ipAddress);
            var response = new TokenResponse();
            response.Id = user.Id;
            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
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
                IsActive = true
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
                    await _mailService.SendAsync(new MailRequest() { From = "darkangelkid1109@gmail.com", To = user.Email, Body = $"Please confirm your account by <a href='{verificationUri}'>clicking here</a>.", Subject = "Confirm Registration" });
                    //string htmlBody = @"<!DOCTYPE html><html><head><title></title><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><meta name='viewport' content='width=device-width, initial-scale=1' /><meta http-equiv='X-UA-Compatible' content='IE=edge' /><style type='text/css'>@media screen{@font-face{font-family:'Lato';font-style:normal;font-weight:400;src:local('Lato Regular'), local('Lato-Regular'), url(https://fonts.gstatic.com/s/lato/v11/qIIYRU-oROkIk8vfvxw6QvesZW2xOQ-xsNqO47m55DA.woff) format('woff')}@font-face{font-family:'Lato';font-style:normal;font-weight:700;src:local('Lato Bold'), local('Lato-Bold'), url(https://fonts.gstatic.com/s/lato/v11/qdgUG4U09HnJwhYI-uK18wLUuEpTyoUstqEm5AMlJo4.woff) format('woff')}@font-face{font-family:'Lato';font-style:italic;font-weight:400;src:local('Lato Italic'), local('Lato-Italic'), url(https://fonts.gstatic.com/s/lato/v11/RYyZNoeFgb0l7W3Vu1aSWOvvDin1pK8aKteLpeZ5c0A.woff) format('woff')}@font-face{font-family:'Lato';font-style:italic;font-weight:700;src:local('Lato Bold Italic'), local('Lato-BoldItalic'), url(https://fonts.gstatic.com/s/lato/v11/HkF_qI1x_noxlxhrhMQYELO3LdcAZYWl9Si6vvxL-qU.woff) format('woff')}}body,table,td,a{-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%}table,td{mso-table-lspace:0pt;mso-table-rspace:0pt}img{-ms-interpolation-mode:bicubic}img{border:0;height:auto;line-height:100%;outline:none;text-decoration:none}table{border-collapse:collapse !important}body{height:100% !important;margin:0 !important;padding:0 !important;width:100% !important}a[x-apple-data-detectors]{color:inherit !important;text-decoration:none !important;font-size:inherit !important;font-family:inherit !important;font-weight:inherit !important;line-height:inherit !important}@media screen and (max-width: 600px){h1{font-size:32px !important;line-height:32px !important}}div[style*='margin: 16px 0;']{margin:0 !important}</style></head><body style=' background-color: #f4f4f4; margin: 0 !important; padding: 0 !important; ' ><div style=' display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: 'Lato', Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden; ' > We're thrilled to have you here! Get ready to dive into your new account.</div><table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td bgcolor='#FFA73B' align='center'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px' ><tr><td align='center' valign='top' style='padding: 40px 10px 40px 10px' ></td></tr></table></td></tr><tr><td bgcolor='#FFA73B' align='center' style='padding: 0px 10px 0px 10px'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px' ><tr><td bgcolor='#ffffff' align='center' valign='top' style=' padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px; ' ><h1 style='font-size: 48px; font-weight: 400; margin: 2'> Welcome!</h1> <img src=' https://img.icons8.com/clouds/100/000000/handshake.png' width='125' height='120' style='display: block; border: 0px' /></td></tr></table></td></tr><tr><td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px' ><tr><td bgcolor='#ffffff' align='left' style=' padding: 20px 30px 40px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px; ' ><p style='margin: 0'> We're excited to have you get started. First, you need to confirm your account. Just press the button below.</p></td></tr><tr><td bgcolor='#ffffff' align='left'><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td bgcolor='#ffffff' align='center' style='padding: 20px 30px 60px 30px' ><table border='0' cellspacing='0' cellpadding='0'><tr><td align='center' style='border-radius: 3px' bgcolor='#FFA73B' > <a href='#' target='_blank' style=' font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; color: #ffffff; text-decoration: none; padding: 15px 25px; border-radius: 2px; border: 1px solid #ffa73b; display: inline-block; ' >Confirm Account</a ></td></tr></table></td></tr></table></td></tr><tr><td bgcolor='#ffffff' align='left' style=' padding: 0px 30px 0px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px; ' ><p style='margin: 0'> If that doesn't work, copy and paste the following link in your browser:</p></td></tr><tr><td bgcolor='#ffffff' align='left' style=' padding: 20px 30px 20px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px; ' ><p style='margin: 0'> <a href='#' target='_blank' style='color: #ffa73b' >https://bit.li.utlddssdstueincx</a ></p></td></tr><tr><td bgcolor='#ffffff' align='left' style=' padding: 0px 30px 20px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px; ' ><p style='margin: 0'> If you have any questions, just reply to this email—we're always happy to help out.</p></td></tr><tr><td bgcolor='#ffffff' align='left' style=' padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px; ' ><p style='margin: 0'>Cheers,<br />BBB Team</p></td></tr></table></td></tr><tr><td bgcolor='#f4f4f4' align='center' style='padding: 30px 10px 0px 10px' ><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px' ><tr><td bgcolor='#FFECD1' align='center' style=' padding: 30px 30px 30px 30px; border-radius: 4px 4px 4px 4px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px; ' ><h2 style=' font-size: 20px; font-weight: 400; color: #111111; margin: 0; ' > Need more help?</h2><p style='margin: 0'> <a href='#' target='_blank' style='color: #ffa73b' >We&rsquo;re here to help you out</a ></p></td></tr></table></td></tr><tr><td bgcolor='#f4f4f4' align='center' style='padding: 0px 10px 0px 10px'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px' ><tr><td bgcolor='#f4f4f4' align='left' style=' padding: 0px 30px 30px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px; ' > <br /><p style='margin: 0'> If these emails get annoying, please feel free to <a href='#' target='_blank' style='color: #111111; font-weight: 700' >unsubscribe</a >.</p></td></tr></table></td></tr></table></body></html>";
                    //await _mailService.SendAsync(new MailRequest() { From = "darkangelkid1109@gmail.com", To = user.Email, Body = htmlBody, Subject = "Xác nhận đăng ký tài khoản" });
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
            await _mailService.SendAsync(emailRequest);
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