using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class CompanyIndustryRepository : ICompanyIndustryRepository
    {
        private readonly IRepositoryAsync<CompanyIndustry> _repository;
        private readonly IDistributedCache _distributedCache;

        public CompanyIndustryRepository(IDistributedCache distributedCache, IRepositoryAsync<CompanyIndustry> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<CompanyIndustry> CompanyIndustries => _repository.Entities;


        public async Task DeleteAsync(CompanyIndustry item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<CompanyIndustry> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<CompanyIndustry>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(CompanyIndustry item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(CompanyIndustry item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
