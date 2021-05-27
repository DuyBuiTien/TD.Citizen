using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class AttributeTextRepository : IAttributeTextRepository
    {
        private readonly IRepositoryAsync<AttributeText> _repository;

        public AttributeTextRepository(IRepositoryAsync<AttributeText> repository)
        {
            _repository = repository;
        }

        public IQueryable<AttributeText> AttributeTexts => _repository.Entities;


        public async Task DeleteAsync(AttributeText item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<AttributeText> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<AttributeText>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(AttributeText item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(AttributeText item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
