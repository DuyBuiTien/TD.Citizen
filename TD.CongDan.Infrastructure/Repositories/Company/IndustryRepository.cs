using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class IndustryRepository : IIndustryRepository
    {
        private readonly IRepositoryAsync<Industry> _repository;
        private readonly IDistributedCache _distributedCache;

        public IndustryRepository(IDistributedCache distributedCache, IRepositoryAsync<Industry> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Industry> Industries => _repository.Entities;


        public async Task DeleteAsync(Industry item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<Industry> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Industry>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Industry item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(Industry item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
