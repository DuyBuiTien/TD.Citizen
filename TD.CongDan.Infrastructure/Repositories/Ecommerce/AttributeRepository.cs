using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly IRepositoryAsync<Attribute> _repository;

        public AttributeRepository( IRepositoryAsync<Attribute> repository)
        {
            _repository = repository;
        }

        public IQueryable<Attribute> Attributes => _repository.Entities;


        public async Task DeleteAsync(Attribute item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<Attribute> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Attribute>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Attribute item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(Attribute item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
