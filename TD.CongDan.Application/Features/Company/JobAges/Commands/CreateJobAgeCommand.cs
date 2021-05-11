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

namespace TD.CongDan.Application.Features.JobAges.Commands
{
    public partial class CreateJobAgeCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class CreateJobAgeCommandHandler : IRequestHandler<CreateJobAgeCommand, Result<int>>
    {
        private readonly IJobAgeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _authenticatedUser;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateJobAgeCommandHandler(IJobAgeRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<int>> Handle(CreateJobAgeCommand request, CancellationToken cancellationToken)
        {
            var id = _authenticatedUser.UserId;
           
            
            var category = _mapper.Map<JobAge>(request);
            await _repository.InsertAsync(category);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(category.Id);
        }
    }
}
