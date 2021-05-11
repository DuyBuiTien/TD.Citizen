using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class BenefitRepository : IBenefitRepository
    {
        private readonly IRepositoryAsync<Benefit> _repository;
        private readonly IDistributedCache _distributedCache;

        public BenefitRepository(IDistributedCache distributedCache, IRepositoryAsync<Benefit> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Benefit> Benefits => _repository.Entities;


        public async Task DeleteAsync(Benefit item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<Benefit> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Benefit>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Benefit item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(Benefit item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
