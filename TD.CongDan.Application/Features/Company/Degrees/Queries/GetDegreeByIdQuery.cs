using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.Degrees.Queries
{
    public class GetDegreeByIdQuery : IRequest<Result<DegreesResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetDegreeByIdQuery, Result<DegreesResponse>>
        {
            private readonly IDegreeRepository _repository;
            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(IDegreeRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<DegreesResponse>> Handle(GetDegreeByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<DegreesResponse>(item);
                return Result<DegreesResponse>.Success(mappedCategory);
            }
        }
    }
}