using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Features.JobAges.Queries;

namespace TD.CongDan.Application.Features.JobAges.Queries
{
    public class GetJobAgeByIdQuery : IRequest<Result<JobAgesResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetJobAgeByIdQuery, Result<JobAgesResponse>>
        {
            private readonly IJobAgeRepository _repository;
            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(IJobAgeRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<JobAgesResponse>> Handle(GetJobAgeByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<JobAgesResponse>(item);
                return Result<JobAgesResponse>.Success(mappedCategory);
            }
        }
    }
}