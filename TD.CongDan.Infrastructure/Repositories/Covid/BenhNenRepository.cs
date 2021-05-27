using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class BenhNenRepository : IBenhNenRepository
    {
        private readonly IRepositoryAsync<BenhNen> _repository;
        private readonly IDistributedCache _distributedCache;

        public BenhNenRepository(IDistributedCache distributedCache, IRepositoryAsync<BenhNen> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<BenhNen> BenhNens => _repository.Entities;


        public async Task DeleteAsync(BenhNen item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<BenhNen> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<BenhNen>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(BenhNen item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(BenhNen item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
