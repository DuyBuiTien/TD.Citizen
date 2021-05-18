using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class JobAppliedRepository : IJobAppliedRepository
    {
        private readonly IRepositoryAsync<JobApplied> _repository;
        private readonly IDistributedCache _distributedCache;

        public JobAppliedRepository(IDistributedCache distributedCache, IRepositoryAsync<JobApplied> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<JobApplied> JobApplieds => _repository.Entities;


        public async Task DeleteAsync(JobApplied item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<JobApplied> GetByIdAsync(string UserName, int RecruitmentId)
        {
            return await _repository.Entities.Where(p => p.UserName == UserName && p.RecruitmentId == RecruitmentId).FirstOrDefaultAsync();
        }

        public async Task<List<JobApplied>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(JobApplied item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(JobApplied item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
