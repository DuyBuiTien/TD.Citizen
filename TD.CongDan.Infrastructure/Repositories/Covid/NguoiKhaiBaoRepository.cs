using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class NguoiKhaiBaoRepository : INguoiKhaiBaoRepository
    {
        private readonly IRepositoryAsync<NguoiKhaiBao> _repository;
        private readonly IDistributedCache _distributedCache;

        public NguoiKhaiBaoRepository(IDistributedCache distributedCache, IRepositoryAsync<NguoiKhaiBao> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<NguoiKhaiBao> NguoiKhaiBaos => _repository.Entities;


        public async Task DeleteAsync(NguoiKhaiBao item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<NguoiKhaiBao> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<NguoiKhaiBao>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(NguoiKhaiBao item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(NguoiKhaiBao item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
