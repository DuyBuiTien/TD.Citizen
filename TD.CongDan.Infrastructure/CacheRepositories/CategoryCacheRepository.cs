using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Infrastructure.CacheKeys;
using TD.Libs.Extensions.Caching;
using TD.Libs.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Infrastructure.CacheRepositories
{
    public class CategoryCacheRepository : ICategoryCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryCacheRepository(IDistributedCache distributedCache, ICategoryRepository categoryRepository)
        {
            _distributedCache = distributedCache;
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetByIdAsync(int categoryId)
        {
            string cacheKey = CategoryCacheKeys.GetKey(categoryId);
            var category = await _distributedCache.GetAsync<Category>(cacheKey);
            if (category == null)
            {
                category = await _categoryRepository.GetByIdAsync(categoryId);
                Throw.Exception.IfNull(category, "Product", "No Product Found");
                await _distributedCache.SetAsync(cacheKey, category);
            }
            return category;
        }

        public async Task<List<Category>> GetCachedListAsync()
        {
            string cacheKey = CategoryCacheKeys.ListKey;
            var categoryList = await _distributedCache.GetAsync<List<Category>>(cacheKey);
            if (categoryList == null)
            {
                categoryList = await _categoryRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, categoryList);
            }
            return categoryList;
        }
    }
}