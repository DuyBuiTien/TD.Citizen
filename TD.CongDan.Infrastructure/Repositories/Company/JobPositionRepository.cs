using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class JobPositionRepository : IJobPositionRepository
    {
        private readonly IRepositoryAsync<JobPosition> _repository;
        private readonly IDistributedCache _distributedCache;

        public JobPositionRepository(IDistributedCache distributedCache, IRepositoryAsync<JobPosition> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<JobPosition> JobPositions => _repository.Entities;


        public async Task DeleteAsync(JobPosition item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<JobPosition> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<JobPosition>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(JobPosition item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(JobPosition item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
