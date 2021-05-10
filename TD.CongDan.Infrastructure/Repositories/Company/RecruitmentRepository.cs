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
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Recruitment>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Recruitment item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(Recruitment item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
