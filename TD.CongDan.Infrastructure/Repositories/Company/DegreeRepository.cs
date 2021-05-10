using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class DegreeRepository : IDegreeRepository
    {
        private readonly IRepositoryAsync<Degree> _repository;
        private readonly IDistributedCache _distributedCache;

        public DegreeRepository(IDistributedCache distributedCache, IRepositoryAsync<Degree> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Degree> Degrees => _repository.Entities;


        public async Task DeleteAsync(Degree item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<Degree> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Degree>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Degree item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(Degree item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
