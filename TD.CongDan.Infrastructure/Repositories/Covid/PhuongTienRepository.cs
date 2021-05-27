using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class PhuongTienRepository : IPhuongTienRepository
    {
        private readonly IRepositoryAsync<PhuongTien> _repository;
        private readonly IDistributedCache _distributedCache;

        public PhuongTienRepository(IDistributedCache distributedCache, IRepositoryAsync<PhuongTien> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<PhuongTien> PhuongTiens => _repository.Entities;


        public async Task DeleteAsync(PhuongTien item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<PhuongTien> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<PhuongTien>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(PhuongTien item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(PhuongTien item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
