using TD.Libs.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using Microsoft.AspNetCore.Identity;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.JobApplications.Commands
{
    public class DeleteJobApplicationCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteCommandHandler : IRequestHandler<DeleteJobApplicationCommand, Result<int>>
        {
            private readonly IJobApplicationRepository _repository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IAuthenticatedUserService _authenticatedUser;
            private readonly UserManager<ApplicationUser> _userManager;


            public DeleteCommandHandler(IJobApplicationRepository repository, IUnitOfWork unitOfWork, IAuthenticatedUserService _authenticatedUser, UserManager<ApplicationUser> _userManager)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                this._authenticatedUser = _authenticatedUser;
                this._userManager = _userManager;
            }

            public async Task<Result<int>> Handle(DeleteJobApplicationCommand command, CancellationToken cancellationToken)
            {
                var userName = _authenticatedUser.Username;
                var user = await _userManager.FindByNameAsync(userName);
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

                var item = await _repository.GetByIdAsync(command.Id);


                if (item != null && (rolesList.Contains("Admin") || rolesList.Contains("SuperAdmin") || userName == item.UserName))
                {
                   
                    await _repository.DeleteAsync(item);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(item.Id);
                }

                return Result<int>.Fail("Lỗi!!!");
            }
        }
    }
}
