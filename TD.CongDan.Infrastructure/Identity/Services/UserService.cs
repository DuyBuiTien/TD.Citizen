using TD.Libs.Results;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Application.Constants;
using TD.CongDan.Application.DTOs.Identity;
using TD.CongDan.Application.DTOs.Settings;
using TD.CongDan.Application.Exceptions;
using TD.CongDan.Application.Extensions;
using TD.CongDan.Application.Interfaces;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Application.Interfaces.Contexts;

namespace TD.CongDan.Infrastructure.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMailService _mailService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _applicationDbContext;



        public UserService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            IApplicationDbContext applicationDbContext,
            SignInManager<ApplicationUser> signInManager, IMailService mailService, IAuthenticatedUserService authenticatedUserService, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            _mailService = mailService;
            _authenticatedUserService = authenticatedUserService;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;

        }


        public async Task<PaginatedResult<UserViewModel>> GetAllUsers(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            Expression<Func<ApplicationUser, UserViewModel>> expression = e => new UserViewModel
            {
                Id = e.Id,
                UserName = e.UserName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                FirstName = e.FirstName,
                LastName = e.LastName,
                IsActive = e.IsActive,
            };


            var paginatedList = await _userManager.Users
                .Sort(orderBy)
                .Search(keySearch)
                .Select(expression)
                .ToPaginatedListAsync(pageNumber, pageSize);
            return paginatedList;
        }

        public async Task<Result<ApplicationUserResponse>> GetByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
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



        /*public async Task<Result<List<RoleViewModel>>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var mappedBrands = _mapper.Map<List<RoleViewModel>>(roles);
            return Result<List<RoleViewModel>>.Success(mappedBrands);
        }*/



        public async Task<PaginatedResult<RoleViewModel>> GetRoles(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            Expression<Func<IdentityRole, RoleViewModel>> expression = e => new RoleViewModel
            {
                Id = e.Id,
                Name = e.Name,
               
            };


            var paginatedList = await _roleManager.Roles
                .Sort(orderBy)
                .Search(keySearch)
                .Select(expression)
                .ToPaginatedListAsync(pageNumber, pageSize);
            return paginatedList;
        }






        public async Task<Result<List<string>>> GetRoleByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _userManager.GetRolesAsync(user);
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);


            return Result<List<string>>.Success(rolesList.ToList());
        }

        public async Task<Result<string>> UpdatePermissionsInRole(PermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            //Remove all Claims First
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddPermissionClaim(role, claim.Value);
            }


            return Result<string>.Success("true");

        }

        public async Task<Result<string>> EditRoleByUsername(string username, ManageUserRolesViewModel model)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            // var currentUser = await _userManager.GetUserAsync(User);
            // await _signInManager.RefreshSignInAsync(currentUser);
            //  await Infrastructure.Identity.Seeds.DefaultSuperAdminUser.SeedAsync(_userManager, _roleManager);

            return Result<string>.Success("true");
        }

        public async Task<Result<PermissionViewModel>> GetRoleById(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
                throw new ApiException("Unexpected Error. Id is null or empty!");
            else
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null) throw new ApiException("Unexpected Error. Role not found!");
                /* var roleviewModel = _mapper.Map<RoleViewModel>(role);
                   return Result<RoleViewModel>.Success(roleviewModel);*/


                var model = new PermissionViewModel();
                var allPermissions = new List<RoleClaimsViewModel>();

                allPermissions.GetPermissions(typeof(Permissions.Brands), roleId);
                allPermissions.GetPermissions(typeof(Permissions.Dashboard), roleId);
                allPermissions.GetPermissions(typeof(Permissions.Products), roleId);
                allPermissions.GetPermissions(typeof(Permissions.Users), roleId);


                model.Id = roleId;
                model.Name = role.Name;
                var claims = await _roleManager.GetClaimsAsync(role);
                var claimsModel = _mapper.Map<List<RoleClaimsViewModel>>(claims);
                var allClaimValues = allPermissions.Select(a => a.Value).ToList();
                var roleClaimValues = claimsModel.Select(a => a.Value).ToList();
                var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
                foreach (var permission in allPermissions)
                {
                    if (authorizedClaims.Any(a => a == permission.Value))
                    {
                        permission.Selected = true;
                    }
                }
                model.RoleClaims = _mapper.Map<List<RoleClaimsViewModel>>(allPermissions);
                return Result<PermissionViewModel>.Success(model);


            }


        }

        public async Task<Result<string>> CreateOrUpdateRole(PermissionViewModel role)
        {
            if (role != null && role.Name != "SuperAdmin" && role.Name != "Basic")

            {
                if (string.IsNullOrEmpty(role.Id))
                {
                  await _roleManager.CreateAsync(new IdentityRole(role.Name));
                   
                }
                else
                {
                    var existingRole = await _roleManager.FindByIdAsync(role.Id);
                    existingRole.Name = role.Name;
                    existingRole.NormalizedName = role.Name.ToUpper();
                    await _roleManager.UpdateAsync(existingRole);
                    var claims = await _roleManager.GetClaimsAsync(existingRole);
                    foreach (var claim in claims)
                    {
                        await _roleManager.RemoveClaimAsync(existingRole, claim);
                    }
                    var selectedClaims = role.RoleClaims.Where(a => a.Selected).ToList();
                    foreach (var claim in selectedClaims)
                    {
                        await _roleManager.AddPermissionClaim(existingRole, claim.Value);
                    }
                }
                return Result<string>.Success("Done!!!");
            }
            else
            {
                throw new ApiException("Unexpected Error!");
            }
        }

        public async Task<Result<string>> DeleteRoleById(string id)
        {
            var existingRole = await _roleManager.FindByIdAsync(id);
            if (existingRole.Name != "SuperAdmin" && existingRole.Name != "Basic")
            {
                //TODO Check if Any Users already uses this Role
                bool roleIsNotUsed = true;
                var allUsers = await _userManager.Users.ToListAsync();
                foreach (var user in allUsers)
                {
                    if (await _userManager.IsInRoleAsync(user, existingRole.Name))
                    {
                        roleIsNotUsed = false;
                    }
                }
                if (roleIsNotUsed)
                {
                    await _roleManager.DeleteAsync(existingRole);
                    return Result<string>.Success($"Role {existingRole.Name} deleted.");
                }
                else
                {
                    return Result<string>.Fail("Role is being Used by another User. Cannot Delete.");
                }
            }
            else
            {
                return Result<string>.Fail($"Not allowed to  delete {existingRole.Name} Role.");
            }
           
        }

    
    }
}
