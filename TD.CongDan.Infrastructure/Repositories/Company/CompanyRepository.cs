using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IRepositoryAsync<Company> _repository;
        private readonly IDistributedCache _distributedCache;

        public CompanyRepository(IDistributedCache distributedCache, IRepositoryAsync<Company> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Company> Companies => _repository.Entities;


        public async Task DeleteAsync(Company item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<Company> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Company> GetByUserNameAsync(string UserName)
        {
            return await _repository.Entities.Where(p => p.UserName == UserName).FirstOrDefaultAsync();
        }

        public async Task<List<Company>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Company item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(Company item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
