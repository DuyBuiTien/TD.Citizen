using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class AttributeDecimalRepository : IAttributeDecimalRepository
    {
        private readonly IRepositoryAsync<AttributeDecimal> _repository;

        public AttributeDecimalRepository(IRepositoryAsync<AttributeDecimal> repository)
        {
            _repository = repository;
        }

        public IQueryable<AttributeDecimal> AttributeDecimals => _repository.Entities;


        public async Task DeleteAsync(AttributeDecimal item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<AttributeDecimal> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<AttributeDecimal>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(AttributeDecimal item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(AttributeDecimal item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
