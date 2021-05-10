using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.Libs.Results;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Features.Products.Queries.GetAllCached
{
    public class GetAllCategoriesCachedQuery : IRequest<Result<List<GetAllCategoriesCachedResponse>>>
    {
        public GetAllCategoriesCachedQuery()
        {
        }
    }

    public class GetAllCategoriesCachedQueryHandler : IRequestHandler<GetAllCategoriesCachedQuery, Result<List<GetAllCategoriesCachedResponse>>>
    {
        private readonly ICategoryCacheRepository _categoryCache;
        private readonly IMapper _mapper;

        public GetAllCategoriesCachedQueryHandler(ICategoryCacheRepository categoryCache, IMapper mapper)
        {
            _categoryCache = categoryCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllCategoriesCachedResponse>>> Handle(GetAllCategoriesCachedQuery request, CancellationToken cancellationToken)
        {
            var categoryList = await _categoryCache.GetCachedListAsync();
            var mappedCategories = _mapper.Map<List<GetAllCategoriesCachedResponse>>(categoryList);
            return Result<List<GetAllCategoriesCachedResponse>>.Success(mappedCategories);
        }
    }
}