using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Application.Interfaces.Shared;
using System;

namespace TD.CongDan.Application.Features.LicensePlates.Commands
{
    public partial class UpdateLicensePlateCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //Ten chu xe
        public string OwnerFullName { get; set; }
        //Bien so dang ky
        public string LicensePlateNumber { get; set; }
        //Ngay dang ky
        public DateTime? DateOfRegistration { get; set; }

        public string Description { get; set; }
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateLicensePlateCommand, Result<int>>
    {
        private readonly ILicensePlateRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticatedUserService _authenticatedUser;


        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCommandHandler(ILicensePlateRepository repository, IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> _userManager, IAuthenticatedUserService _authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this._userManager = _userManager;
            this._authenticatedUser = _authenticatedUser;
        }

        public async Task<Result<int>> Handle(UpdateLicensePlateCommand command, CancellationToken cancellationToken)
        {
            var userName = _authenticatedUser.Username;
            var user = await _userManager.FindByNameAsync(userName);
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            var item = await _repository.GetByIdAsync(command.Id);

            if (item == null || !(rolesList.Contains("Admin") || rolesList.Contains("SuperAdmin") || userName == item.UserName))
            {
                return Result<int>.Fail($"Lỗi!");
            }
     
            else
            {
                item.Name = command.Name ?? item.Name;
                item.Description = command.Description ?? item.Description;
                item.OwnerFullName = command.OwnerFullName ?? item.OwnerFullName;
                item.LicensePlateNumber = command.LicensePlateNumber ?? item.LicensePlateNumber;
                item.DateOfRegistration = command.DateOfRegistration ?? item.DateOfRegistration;
                item.Description = command.Description ?? item.Description;

                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
        }
    }
}

