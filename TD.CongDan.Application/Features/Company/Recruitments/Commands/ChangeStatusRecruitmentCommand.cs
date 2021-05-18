using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;
using System.Globalization;
using TD.CongDan.Application.Features.Places.Commands;
using TD.Libs.ThrowR;
using Microsoft.AspNetCore.Identity;

namespace TD.CongDan.Application.Features.Recruitments.Commands
{
    public partial class ChangeStatusRecruitmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int Status { get; set; }

    }



    public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusRecruitmentCommand, Result<int>>
    {
        private readonly IRecruitmentRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public ChangeStatusCommandHandler(IRecruitmentRepository repository, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
            _userManager = userManager;
            
        }

        public async Task<Result<int>> Handle(ChangeStatusRecruitmentCommand request, CancellationToken cancellationToken)
        {
            var userName = _authenticatedUser.Username;
            var user = await _userManager.FindByNameAsync(userName);
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            var item = await _repository.GetByIdAsync(request.Id);

            if (item == null || !(rolesList.Contains("Admin") || rolesList.Contains("SuperAdmin") || userName == item.UserName))
            {
                return Result<int>.Fail($"Lỗi!");
            }
            else
            {
                item.Status = request.Status >=0 ? request.Status : item.Status;
                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
    
        }
    }
}
