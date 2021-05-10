using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly IRepositoryAsync<Salary> _repository;
        private readonly IDistributedCache _distributedCache;

        public SalaryRepository(IDistributedCache distributedCache, IRepositoryAsync<Salary> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Salary>Salaries => _repository.Entities;


        public async Task DeleteAsync(Salary item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<Salary> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Salary>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Salary item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(Salary item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
