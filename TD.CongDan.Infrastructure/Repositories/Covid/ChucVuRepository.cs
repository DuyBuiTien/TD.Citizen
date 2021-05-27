using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class ChucVuRepository : IChucVuRepository
    {
        private readonly IRepositoryAsync<ChucVu> _repository;
        private readonly IDistributedCache _distributedCache;

        public ChucVuRepository(IDistributedCache distributedCache, IRepositoryAsync<ChucVu> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<ChucVu> ChucVus => _repository.Entities;


        public async Task DeleteAsync(ChucVu item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<ChucVu> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<ChucVu>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(ChucVu item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(ChucVu item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
