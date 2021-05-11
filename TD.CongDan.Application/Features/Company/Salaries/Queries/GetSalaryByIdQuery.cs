using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.Salaries.Queries
{
    public class GetSalaryByIdQuery : IRequest<Result<SalariesResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetSalaryByIdQuery, Result<SalariesResponse>>
        {
            private readonly ISalaryRepository _repository;
            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(ISalaryRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<SalariesResponse>> Handle(GetSalaryByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<SalariesResponse>(item);
                return Result<SalariesResponse>.Success(mappedCategory);
            }
        }
    }
}