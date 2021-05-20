using TD.Libs.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Application.Interfaces.Shared;

namespace TD.CongDan.Application.Features.LicensePlates.Commands
{
    public class DeleteLicensePlateCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeletePlaceTypeCommandHandler : IRequestHandler<DeleteLicensePlateCommand, Result<int>>
        {
            private readonly ILicensePlateRepository _repository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IAuthenticatedUserService _authenticatedUser;

            public DeletePlaceTypeCommandHandler(ILicensePlateRepository repository, IUnitOfWork unitOfWork, UserManager<ApplicationUser> _userManager, IAuthenticatedUserService _authenticatedUser)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                this._userManager = _userManager;
                this._authenticatedUser = _authenticatedUser;
            }

            public async Task<Result<int>> Handle(DeleteLicensePlateCommand command, CancellationToken cancellationToken)
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
                    await _repository.DeleteAsync(item);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(item.Id);
                }
            }
        }
    }
}
