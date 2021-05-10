using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly IRepositoryAsync<Experience> _repository;
        private readonly IDistributedCache _distributedCache;

        public ExperienceRepository(IDistributedCache distributedCache, IRepositoryAsync<Experience> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Experience> Experiences => _repository.Entities;


        public async Task DeleteAsync(Experience item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<Experience> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Experience>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Experience item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(Experience item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
