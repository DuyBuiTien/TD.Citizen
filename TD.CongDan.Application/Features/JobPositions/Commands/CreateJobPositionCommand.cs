﻿using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Features.JobPositions.Commands
{
    public partial class CreateJobPositionCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class CreateCommandHandler : IRequestHandler<CreateJobPositionCommand, Result<int>>
    {
        private readonly IJobPositionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateCommandHandler(IJobPositionRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<int>> Handle(CreateJobPositionCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.UserId;
           
            
            var item = _mapper.Map<JobPosition>(request);
            await _repository.InsertAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(item.Id);
        }
    }
}
