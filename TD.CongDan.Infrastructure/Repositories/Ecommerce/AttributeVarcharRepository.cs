using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class AttributeVarcharRepository : IAttributeVarcharRepository
    {
        private readonly IRepositoryAsync<AttributeVarchar> _repository;

        public AttributeVarcharRepository(IRepositoryAsync<AttributeVarchar> repository)
        {
            _repository = repository;
        }

        public IQueryable<AttributeVarchar> AttributeVarchars => _repository.Entities;


        public async Task DeleteAsync(AttributeVarchar item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<AttributeVarchar> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<AttributeVarchar>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(AttributeVarchar item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(AttributeVarchar item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
