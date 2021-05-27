using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class ChotKiemDichRepository : IChotKiemDichRepository
    {
        private readonly IRepositoryAsync<ChotKiemDich> _repository;
        private readonly IDistributedCache _distributedCache;

        public ChotKiemDichRepository(IDistributedCache distributedCache, IRepositoryAsync<ChotKiemDich> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<ChotKiemDich> ChotKiemDichs => _repository.Entities;


        public async Task DeleteAsync(ChotKiemDich item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<ChotKiemDich> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<ChotKiemDich>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(ChotKiemDich item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(ChotKiemDich item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
