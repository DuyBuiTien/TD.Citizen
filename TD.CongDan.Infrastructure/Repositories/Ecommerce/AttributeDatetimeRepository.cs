using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class AttributeDatetimeRepository : IAttributeDatetimeRepository
    {
        private readonly IRepositoryAsync<AttributeDatetime> _repository;

        public AttributeDatetimeRepository(IRepositoryAsync<AttributeDatetime> repository)
        {
            _repository = repository;
        }

        public IQueryable<AttributeDatetime> AttributeDatetimes => _repository.Entities;


        public async Task DeleteAsync(AttributeDatetime item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<AttributeDatetime> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<AttributeDatetime>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(AttributeDatetime item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(AttributeDatetime item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
