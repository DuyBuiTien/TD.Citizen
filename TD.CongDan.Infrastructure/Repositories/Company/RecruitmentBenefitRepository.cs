using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class RecruitmentBenefitRepository : IRecruitmentBenefitRepository
    {
        private readonly IRepositoryAsync<RecruitmentBenefit> _repository;
        private readonly IDistributedCache _distributedCache;

        public RecruitmentBenefitRepository(IDistributedCache distributedCache, IRepositoryAsync<RecruitmentBenefit> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<RecruitmentBenefit> RecruitmentBenefits => _repository.Entities;


        public async Task DeleteAsync(RecruitmentBenefit item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<RecruitmentBenefit> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<RecruitmentBenefit>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(RecruitmentBenefit item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(RecruitmentBenefit item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
