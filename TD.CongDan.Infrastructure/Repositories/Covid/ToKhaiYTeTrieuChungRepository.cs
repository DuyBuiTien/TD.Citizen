using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class ToKhaiYTeTrieuChungRepository : IToKhaiYTeTrieuChungRepository
    {
        private readonly IRepositoryAsync<ToKhaiYTeTrieuChung> _repository;
        private readonly IDistributedCache _distributedCache;

        public ToKhaiYTeTrieuChungRepository(IDistributedCache distributedCache, IRepositoryAsync<ToKhaiYTeTrieuChung> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<ToKhaiYTeTrieuChung> ToKhaiYTeTrieuChungs => _repository.Entities;


        public async Task DeleteAsync(ToKhaiYTeTrieuChung item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<ToKhaiYTeTrieuChung> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<ToKhaiYTeTrieuChung>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(ToKhaiYTeTrieuChung item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(ToKhaiYTeTrieuChung item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
