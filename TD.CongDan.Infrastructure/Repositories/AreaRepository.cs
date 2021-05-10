using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly IRepositoryAsync<Area> _repository;
        private readonly IDistributedCache _distributedCache;

        public AreaRepository(IDistributedCache distributedCache, IRepositoryAsync<Area> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Area> Areas => _repository.Entities;

        public async Task DeleteAsync(Area item)
        {
            await _repository.DeleteAsync(item);
            await _distributedCache.RemoveAsync(CacheKeys.AreaCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.AreaCacheKeys.GetKey(item.Id));
        }

        public async Task<Area> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Area> GetByCodeAsync(string code)
        {
            return await _repository.Entities.Where(p => p.Code == code).FirstOrDefaultAsync();
        }
        public async Task<List<Area>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Area item)
        {
            await _repository.AddAsync(item);
            await _distributedCache.RemoveAsync(CacheKeys.AreaCacheKeys.ListKey);
            return item.Id;
        }

        public async Task UpdateAsync(Area item)
        {
            await _repository.UpdateAsync(item);
            await _distributedCache.RemoveAsync(CacheKeys.AreaCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.AreaCacheKeys.GetKey(item.Id));
        }
    }
}
