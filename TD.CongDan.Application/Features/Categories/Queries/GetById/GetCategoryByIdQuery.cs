using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Features.Categories.Queries.GetById
{
    public class GetCategoryByIdQuery : IRequest<Result<GetCategoryByIdResponse>>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryByIdResponse>>
        {
            private readonly ICategoryCacheRepository _categoryCache;
            private readonly IMapper _mapper;

            public GetCategoryByIdQueryHandler(ICategoryCacheRepository categoryCache, IMapper mapper)
            {
                _categoryCache = categoryCache;
                _mapper = mapper;
            }

            public async Task<Result<GetCategoryByIdResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
            {
                var category = await _categoryCache.GetByIdAsync(query.Id);
                var mappedCategory = _mapper.Map<GetCategoryByIdResponse>(category);
                return Result<GetCategoryByIdResponse>.Success(mappedCategory);
            }
        }
    }
}