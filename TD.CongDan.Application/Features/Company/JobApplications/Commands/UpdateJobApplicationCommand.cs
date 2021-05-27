using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using TD.CongDan.Application.Interfaces.Shared;

namespace TD.CongDan.Application.Features.JobApplications.Commands
{
    public partial class UpdateJobApplicationCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CVFile { get; set; }
        //Vi tri hien tai
        public int? CurrentPositionId { get; set; }
        //Vi tri mong muon
        public int? PositionId { get; set; }
        public int? JobNameId { get; set; }
        //Trinh do hoc van
        public int? DegreeId { get; set; }
        //Tong so nam Kinh nghiem
        public int? ExperienceId { get; set; }
        //Mong muon muc luong toi thieu
        public int? MinExpectedSalary { get; set; }
        //Dia diem lam viec
        public string Adrress { get; set; }
        //Hinh thuc lam viec
        public int? JobTypeId { get; set; }
        //Cho phep nguoi khac tim kiem thong tin
        public int? IsSearchAllowed { get; set; }
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateJobApplicationCommand, Result<int>>
    {
        private readonly IJobApplicationRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateCommandHandler(UserManager<ApplicationUser> _userManager, IAuthenticatedUserService _authenticatedUser, IJobApplicationRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this._authenticatedUser = _authenticatedUser;
            this._userManager = _userManager;
        }

        public async Task<Result<int>> Handle(UpdateJobApplicationCommand command, CancellationToken cancellationToken)
        {
            var userName = _authenticatedUser.Username;
            var user = await _userManager.FindByNameAsync(userName);
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var item = await _repository.GetByIdAsync(command.Id);

            if (item != null && (rolesList.Contains("Admin") || rolesList.Contains("SuperAdmin") || userName == item.UserName))
            {
                item.Name = command.Name ?? item.Name;
                item.CVFile = command.CVFile ?? item.CVFile;
                item.CurrentPositionId = command.CurrentPositionId ?? item.CurrentPositionId;
                item.PositionId = command.PositionId ?? item.PositionId;
                item.DegreeId = command.DegreeId ?? item.DegreeId;
                item.ExperienceId = command.ExperienceId ?? item.ExperienceId;
                item.MinExpectedSalary = command.MinExpectedSalary ?? item.MinExpectedSalary;
                item.Adrress = command.Adrress ?? item.Adrress;
                item.JobTypeId = command.JobTypeId ?? item.JobTypeId;
                item.IsSearchAllowed = command.IsSearchAllowed ?? item.IsSearchAllowed;
                await _repository.UpdateAsync(item);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(item.Id);
            }
            return Result<int>.Fail("Lỗi!!!");
        }
    }
}
