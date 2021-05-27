using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class ToKhaiYTeRepository : IToKhaiYTeRepository
    {
        private readonly IRepositoryAsync<ToKhaiYTe> _repository;
        private readonly IDistributedCache _distributedCache;

        public ToKhaiYTeRepository(IDistributedCache distributedCache, IRepositoryAsync<ToKhaiYTe> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<ToKhaiYTe> ToKhaiYTes => _repository.Entities;


        public async Task DeleteAsync(ToKhaiYTe item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<ToKhaiYTe> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<ToKhaiYTe>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(ToKhaiYTe item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(ToKhaiYTe item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
