using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class JobNameRepository : IJobNameRepository
    {
        private readonly IRepositoryAsync<JobName> _repository;
        private readonly IDistributedCache _distributedCache;

        public JobNameRepository(IDistributedCache distributedCache, IRepositoryAsync<JobName> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<JobName> JobNames => _repository.Entities;


        public async Task DeleteAsync(JobName item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<JobName> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<JobName>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(JobName item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(JobName item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
