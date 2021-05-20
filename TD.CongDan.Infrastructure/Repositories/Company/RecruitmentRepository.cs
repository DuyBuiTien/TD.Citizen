using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class RecruitmentRepository : IRecruitmentRepository
    {
        private readonly IRepositoryAsync<Recruitment> _repository;
        private readonly IDistributedCache _distributedCache;

        public RecruitmentRepository(IDistributedCache distributedCache, IRepositoryAsync<Recruitment> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Recruitment> Recruitments => _repository.Entities;


        public async Task DeleteAsync(Recruitment item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<Recruitment> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).Include(x =>x.Degree)
                .Include(x=>x.Place)
                .Include(x => x.Company)
                .Include(x => x.Gender)
                .Include(x => x.JobPosition)
                .Include(x => x.JobType)
                .Include(x => x.JobName)
                .Include(x => x.Salary)
                .Include(x => x.JobAge)
                .Include(x => x.Degree)
                .Include(x => x.Experience)
                .Include(x => x.RecruitmentBenefit)
                .Include(x => x.Place).ThenInclude(x => x.Province).Include(x => x.Place).ThenInclude(x => x.District).Include(x => x.Place).ThenInclude(x => x.Commune).Include(x => x.Place)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Recruitment>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Recruitment item)
        {
            var item_ = item;
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(Recruitment item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
