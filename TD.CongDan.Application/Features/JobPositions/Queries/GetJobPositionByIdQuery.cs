using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.JobPositions.Queries
{
    public class GetJobPositionByIdQuery : IRequest<Result<JobPositionsResponse>>
    {
        public int Id { get; set; }

        public class GetByIdQueryHandler : IRequestHandler<GetJobPositionByIdQuery, Result<JobPositionsResponse>>
        {
            private readonly IJobPositionRepository _repository;
            private readonly IMapper _mapper;

            public GetByIdQueryHandler(IJobPositionRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<JobPositionsResponse>> Handle(GetJobPositionByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<JobPositionsResponse>(item);
                return Result<JobPositionsResponse>.Success(mappedCategory);
            }
        }
    }
}