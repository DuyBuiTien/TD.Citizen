using TD.Libs.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using TD.CongDan.Application.Interfaces.Shared;

namespace TD.CongDan.Application.Features.Recruitments.Commands
{
    public class DeleteRecruitmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteCommandHandler : IRequestHandler<DeleteRecruitmentCommand, Result<int>>
        {
            private readonly IRecruitmentRepository _repository;
            private readonly IRecruitmentBenefitRepository _recruitmentBenefitRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IAuthenticatedUserService _authenticatedUser;


            public DeleteCommandHandler(IRecruitmentRepository repository, IAuthenticatedUserService authenticatedUser, UserManager<ApplicationUser> userManager, IRecruitmentBenefitRepository recruitmentBenefitRepository, IUnitOfWork unitOfWork)
            {
                _repository = repository;
                _unitOfWork = unitOfWork;
                _recruitmentBenefitRepository = recruitmentBenefitRepository;
                _userManager = userManager;
                _authenticatedUser = authenticatedUser;
            }

            public async Task<Result<int>> Handle(DeleteRecruitmentCommand command, CancellationToken cancellationToken)
            {

                var userName = _authenticatedUser.Username;
                var user = await _userManager.FindByNameAsync(userName);
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

                var item = await _repository.GetByIdAsync(command.Id);


                if (item != null && (rolesList.Contains("Admin") || rolesList.Contains("SuperAdmin") || userName== item.UserName))
                {
                    var listrecruitmentBenefit = _recruitmentBenefitRepository.RecruitmentBenefits.Where(s => s.RecruitmentId == item.Id).ToList();
                    foreach (var _item in listrecruitmentBenefit)
                    {
                            await _recruitmentBenefitRepository.DeleteAsync(_item);
                    }
                    await _repository.DeleteAsync(item);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(item.Id);
                }

                return Result<int>.Fail("Lỗi!!!");

            }
        }
    }
}
