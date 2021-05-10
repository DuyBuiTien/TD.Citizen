using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.Libs.Extensions.Caching;
using TD.Libs.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Infrastructure.CacheKeys;

namespace TD.CongDan.Infrastructure.CacheRepositories
{
    public class AreaCacheRepository : IAreaCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IAreaRepository _repository;

        public AreaCacheRepository(IDistributedCache distributedCache, IAreaRepository repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public async Task<Area> GetByIdAsync(int Id)
        {
            string cacheKey = AreaCacheKeys.GetKey(Id);
            var item = await _distributedCache.GetAsync<Area>(cacheKey);
            if (item == null)
            {
                item = await _repository.GetByIdAsync(Id);
                Throw.Exception.IfNull(item, "Area", "No Area Found");
                await _distributedCache.SetAsync(cacheKey, item);
            }
            return item;
        }

        public async Task<List<Area>> GetCachedListAsync()
        {
            string cacheKey = AreaCacheKeys.ListKey;
            var list = await _distributedCache.GetAsync<List<Area>>(cacheKey);
            if (list == null)
            {
                list = await _repository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, list);
            }
            return list;
        }
    }
}