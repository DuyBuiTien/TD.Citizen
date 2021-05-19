using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Features.JobAges.Queries;

namespace TD.CongDan.Application.Features.JobApplications.Queries
{
    public class GetJobApplicationByIdQuery : IRequest<Result<JobApplicationResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetJobApplicationByIdQuery, Result<JobApplicationResponse>>
        {
            private readonly IJobApplicationRepository _repository;
            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(IJobApplicationRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<JobApplicationResponse>> Handle(GetJobApplicationByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<JobApplicationResponse>(item);
                return Result<JobApplicationResponse>.Success(mappedCategory);
            }
        }
    }
}