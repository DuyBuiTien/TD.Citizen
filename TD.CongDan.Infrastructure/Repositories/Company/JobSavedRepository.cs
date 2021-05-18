using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class JobSavedRepository : IJobSavedRepository
    {
        private readonly IRepositoryAsync<JobSaved> _repository;
        private readonly IDistributedCache _distributedCache;

        public JobSavedRepository(IDistributedCache distributedCache, IRepositoryAsync<JobSaved> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<JobSaved> JobSaveds => _repository.Entities;


        public async Task DeleteAsync(JobSaved item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<JobSaved> GetByIdAsync(string UserName, int RecruitmentId)
        {
            return await _repository.Entities.Where(p => p.UserName== UserName && p.RecruitmentId==RecruitmentId).FirstOrDefaultAsync();
        }

        public async Task<List<JobSaved>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(JobSaved item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(JobSaved item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
