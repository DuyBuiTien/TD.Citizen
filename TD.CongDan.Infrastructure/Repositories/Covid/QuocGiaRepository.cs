using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class QuocGiaRepository : IQuocGiaRepository
    {
        private readonly IRepositoryAsync<QuocGia> _repository;
        private readonly IDistributedCache _distributedCache;

        public QuocGiaRepository(IDistributedCache distributedCache, IRepositoryAsync<QuocGia> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<QuocGia> QuocGias => _repository.Entities;


        public async Task DeleteAsync(QuocGia item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<QuocGia> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<QuocGia>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(QuocGia item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(QuocGia item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
