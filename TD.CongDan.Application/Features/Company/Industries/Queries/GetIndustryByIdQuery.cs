using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Application.Features.JobAges.Queries;

namespace TD.CongDan.Application.Features.Industries.Queries
{
    public class GetIndustryByIdQuery : IRequest<Result<IndustriesResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetIndustryByIdQuery, Result<IndustriesResponse>>
        {
            private readonly IIndustryRepository _repository;
            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(IIndustryRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<IndustriesResponse>> Handle(GetIndustryByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<IndustriesResponse>(item);
                return Result<IndustriesResponse>.Success(mappedCategory);
            }
        }
    }
}