using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly IRepositoryAsync<JobApplication> _repository;
        private readonly IDistributedCache _distributedCache;

        public JobApplicationRepository(IDistributedCache distributedCache, IRepositoryAsync<JobApplication> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<JobApplication> JobApplications => _repository.Entities;


        public async Task DeleteAsync(JobApplication item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<JobApplication> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).Include(x => x.Degree).Include(x => x.CurrentPosition).Include(x => x.Position).Include(x => x.JobType).Include(x => x.Experience).FirstOrDefaultAsync();
        }

        public async Task<JobApplication> GetByUsernameAsync(string UserName)
        {
            return await _repository.Entities.Where(p => p.UserName == UserName).Include(x => x.Degree).Include(x => x.CurrentPosition).Include(x => x.Position).Include(x => x.JobType).Include(x => x.Experience).FirstOrDefaultAsync();
        }

        public async Task<List<JobApplication>> GetListAsync()
        {
            return await _repository.Entities.Include(x=>x.Degree).Include(x => x.CurrentPosition).Include(x => x.Position).Include(x => x.JobType).Include(x => x.Experience).ToListAsync();
        }

        public async Task<int> InsertAsync(JobApplication item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(JobApplication item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
