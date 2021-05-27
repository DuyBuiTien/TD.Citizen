using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class DonViCongTacRepository : IDonViCongTacRepository
    {
        private readonly IRepositoryAsync<DonViCongTac> _repository;
        private readonly IDistributedCache _distributedCache;

        public DonViCongTacRepository(IDistributedCache distributedCache, IRepositoryAsync<DonViCongTac> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<DonViCongTac> DonViCongTacs => _repository.Entities;


        public async Task DeleteAsync(DonViCongTac item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<DonViCongTac> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<DonViCongTac>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(DonViCongTac item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(DonViCongTac item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
