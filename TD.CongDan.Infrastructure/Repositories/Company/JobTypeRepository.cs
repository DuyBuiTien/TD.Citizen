using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class JobTypeRepository : IJobTypeRepository
    {
        private readonly IRepositoryAsync<JobType> _repository;
        private readonly IDistributedCache _distributedCache;

        public JobTypeRepository(IDistributedCache distributedCache, IRepositoryAsync<JobType> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<JobType> JobTypes => _repository.Entities;


        public async Task DeleteAsync(JobType item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<JobType> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<JobType>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(JobType item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(JobType item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
