using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.EcommerceCategories.Queries
{
    public class GetEcommerceCategoryByIdQuery : IRequest<Result<EcommerceCategoryResponse>>
    {
        public int Id { get; set; }

        public class GetAreaByIdQueryHandler : IRequestHandler<GetEcommerceCategoryByIdQuery, Result<EcommerceCategoryResponse>>
        {
            private readonly IEcommerceCategoryRepository _repository;
            private readonly IMapper _mapper;

            public GetAreaByIdQueryHandler(IEcommerceCategoryRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<EcommerceCategoryResponse>> Handle(GetEcommerceCategoryByIdQuery query, CancellationToken cancellationToken)
            {
                var item = await _repository.GetByIdAsync(query.Id);
                var mapped = _mapper.Map<EcommerceCategoryResponse>(item);
                return Result<EcommerceCategoryResponse>.Success(mapped);
            }
        }
    }
}