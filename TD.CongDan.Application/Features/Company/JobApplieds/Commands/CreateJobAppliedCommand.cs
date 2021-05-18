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

namespace TD.CongDan.Application.Features.JobApplieds.Commands
{
    public partial class CreateJobAppliedCommand : IRequest<Result<int>>
    {
        public int? RecruitmentId { get; set; }
        public string CVFile { get; set; }

    }

    public class CreateCommandHandler : IRequestHandler<CreateJobAppliedCommand, Result<int>>
    {
        private readonly IJobAppliedRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCommandHandler(IJobAppliedRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<int>> Handle(CreateJobAppliedCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.Username;

            JobApplied item = new JobApplied { RecruitmentId = request.RecruitmentId, UserName = id , CVFile = request.CVFile };
            
            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(item.Id);
        }
    }
}
