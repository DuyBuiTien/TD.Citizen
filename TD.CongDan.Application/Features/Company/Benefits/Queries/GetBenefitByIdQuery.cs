using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.Benefits.Queries
{
    public class GetBenefitByIdQuery : IRequest<Result<BenefitsResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetBenefitByIdQuery, Result<BenefitsResponse>>
        {
            private readonly IBenefitRepository _repository;
            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(IBenefitRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<BenefitsResponse>> Handle(GetBenefitByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<BenefitsResponse>(item);
                return Result<BenefitsResponse>.Success(mappedCategory);
            }
        }
    }
}