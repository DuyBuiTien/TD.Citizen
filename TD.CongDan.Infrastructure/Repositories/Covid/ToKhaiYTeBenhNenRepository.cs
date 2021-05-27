using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class ToKhaiYTeBenhNenRepository : IToKhaiYTeBenhNenRepository
    {
        private readonly IRepositoryAsync<ToKhaiYTeBenhNen> _repository;
        private readonly IDistributedCache _distributedCache;

        public ToKhaiYTeBenhNenRepository(IDistributedCache distributedCache, IRepositoryAsync<ToKhaiYTeBenhNen> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<ToKhaiYTeBenhNen> ToKhaiYTeBenhNens => _repository.Entities;


        public async Task DeleteAsync(ToKhaiYTeBenhNen item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<ToKhaiYTeBenhNen> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<ToKhaiYTeBenhNen>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(ToKhaiYTeBenhNen item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(ToKhaiYTeBenhNen item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
