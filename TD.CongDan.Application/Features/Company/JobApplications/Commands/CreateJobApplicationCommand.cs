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

namespace TD.CongDan.Application.Features.JobApplications.Commands
{
    public partial class CreateJobApplicationCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string CVFile { get; set; }
        //Vi tri hien tai
        public int? CurrentPositionId { get; set; }
        //Vi tri mong muon
        public int? PositionId { get; set; }
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

    public class CreateJobAgeCommandHandler : IRequestHandler<CreateJobApplicationCommand, Result<int>>
    {
        private readonly IJobApplicationRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateJobAgeCommandHandler(IJobApplicationRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<int>> Handle(CreateJobApplicationCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.Username;
           
            
            var item = _mapper.Map<JobApplication>(request);
            item.UserName = id;
            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(item.Id);
        }
    }
}
