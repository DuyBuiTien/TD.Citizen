using TD.Libs.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using Microsoft.AspNetCore.Identity;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.Companies.Commands
{
    public class DeleteCompanyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteCommandHandler : IRequestHandler<DeleteCompanyCommand, Result<int>>
        {
            private readonly ICompanyRepository _repository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IAuthenticatedUserService _authenticatedUser;

            public DeleteCommandHandler(ICompanyRepository repository, IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser, UserManager<ApplicationUser> userManager)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _userManager = userManager;
                _authenticatedUser = authenticatedUser;
            }

            public async Task<Result<int>> Handle(DeleteCompanyCommand command, CancellationToken cancellationToken)
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
