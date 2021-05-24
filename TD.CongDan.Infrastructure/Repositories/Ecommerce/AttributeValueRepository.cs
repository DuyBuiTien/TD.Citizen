using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class AttributeValueRepository : IAttributeValueRepository
    {
        private readonly IRepositoryAsync<AttributeValue> _repository;

        public AttributeValueRepository(IRepositoryAsync<AttributeValue> repository)
        {
            _repository = repository;
        }

        public IQueryable<AttributeValue> AttributeValues => _repository.Entities;


        public async Task DeleteAsync(AttributeValue item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<AttributeValue> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<AttributeValue>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(AttributeValue item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(AttributeValue item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
