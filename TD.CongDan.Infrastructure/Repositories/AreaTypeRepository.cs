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
    public class AreaTypeRepository : IAreaTypeRepository
    {
        private readonly IRepositoryAsync<AreaType> _repository;
        private readonly IDistributedCache _distributedCache;

        public AreaTypeRepository(IDistributedCache distributedCache, IRepositoryAsync<AreaType> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<AreaType> AreaTypes => _repository.Entities;

        public async Task DeleteAsync(AreaType item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<AreaType> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<AreaType> GetByCodeAsync(string code)
        {
            return await _repository.Entities.Where(p => p.Code == code).FirstOrDefaultAsync();
        }

        public async Task<List<AreaType>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(AreaType item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(AreaType item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
