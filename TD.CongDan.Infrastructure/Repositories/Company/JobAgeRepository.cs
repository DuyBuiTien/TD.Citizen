using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class JobAgeRepository : IJobAgeRepository
    {
        private readonly IRepositoryAsync<JobAge> _repository;
        private readonly IDistributedCache _distributedCache;

        public JobAgeRepository(IDistributedCache distributedCache, IRepositoryAsync<JobAge> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<JobAge> JobAges => _repository.Entities;


        public async Task DeleteAsync(JobAge item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<JobAge> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<JobAge>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(JobAge item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(JobAge item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
