using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;

namespace TD.CongDan.Application.Features.QuocGias.Queries
{
    public class GetQuocGiaByIdQuery : IRequest<Result<QuocGiasResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetQuocGiaByIdQuery, Result<QuocGiasResponse>>
        {
            private readonly IQuocGiaRepository _repository;
            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(IQuocGiaRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<QuocGiasResponse>> Handle(GetQuocGiaByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<QuocGiasResponse>(item);
                return Result<QuocGiasResponse>.Success(mappedCategory);
            }
        }
    }
}