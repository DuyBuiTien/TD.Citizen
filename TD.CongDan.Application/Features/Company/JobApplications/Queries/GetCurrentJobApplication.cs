using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Features.JobAges.Queries;
using TD.CongDan.Application.Interfaces.Shared;

namespace TD.CongDan.Application.Features.JobApplications.Queries
{
    public class GetCurrentJobApplication : IRequest<Result<JobApplicationResponse>>
    {
      

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCurrentJobApplication, Result<JobApplicationResponse>>
        {
            private readonly IJobApplicationRepository _repository;
            private readonly IMapper _mapper;
            private readonly IAuthenticatedUserService _authenticatedUser;


            public GetCategoryByIdQueryHandler(IJobApplicationRepository repository, IMapper mapper, IAuthenticatedUserService _authenticatedUser)
            {
                _repository = repository;
                _mapper = mapper;
                this._authenticatedUser = _authenticatedUser;
            }

            public async Task<Result<JobApplicationResponse>> Handle(GetCurrentJobApplication query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByUsernameAsync(_authenticatedUser.Username);
                var mappedCategory = _mapper.Map<JobApplicationResponse>(item);
                return Result<JobApplicationResponse>.Success(mappedCategory);
            }
        }
    }
}