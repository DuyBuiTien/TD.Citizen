using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.JobNames.Queries
{
    public class GetJobNameByIdQuery : IRequest<Result<JobNamesResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetJobNameByIdQuery, Result<JobNamesResponse>>
        {
            private readonly IJobNameRepository _repository;
            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(IJobNameRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<JobNamesResponse>> Handle(GetJobNameByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<JobNamesResponse>(item);
                return Result<JobNamesResponse>.Success(mappedCategory);
            }
        }
    }
}