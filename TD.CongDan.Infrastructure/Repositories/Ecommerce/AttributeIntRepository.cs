using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class AttributeIntRepository : IAttributeIntRepository
    {
        private readonly IRepositoryAsync<AttributeInt> _repository;

        public AttributeIntRepository(IRepositoryAsync<AttributeInt> repository)
        {
            _repository = repository;
        }

        public IQueryable<AttributeInt> AttributeInts => _repository.Entities;


        public async Task DeleteAsync(AttributeInt item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<AttributeInt> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<AttributeInt>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(AttributeInt item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(AttributeInt item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
