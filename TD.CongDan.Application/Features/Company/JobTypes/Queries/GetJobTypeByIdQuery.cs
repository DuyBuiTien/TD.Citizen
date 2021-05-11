using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.JobTypes.Queries
{
    public class GetJobTypeByIdQuery : IRequest<Result<JobTypesResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetJobTypeByIdQuery, Result<JobTypesResponse>>
        {
            private readonly IJobTypeRepository _repository;
            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(IJobTypeRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<JobTypesResponse>> Handle(GetJobTypeByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<JobTypesResponse>(item);
                return Result<JobTypesResponse>.Success(mappedCategory);
            }
        }
    }
}