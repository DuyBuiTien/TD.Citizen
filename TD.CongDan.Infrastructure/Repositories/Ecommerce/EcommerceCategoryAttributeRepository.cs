using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class EcommerceCategoryAttributeRepository : IEcommerceCategoryAttributeRepository
    {
        private readonly IRepositoryAsync<EcommerceCategoryAttribute> _repository;

        public EcommerceCategoryAttributeRepository(IRepositoryAsync<EcommerceCategoryAttribute> repository)
        {
            _repository = repository;
        }

        public IQueryable<EcommerceCategoryAttribute> EcommerceCategoryAttributes => _repository.Entities;


        public async Task DeleteAsync(EcommerceCategoryAttribute item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<EcommerceCategoryAttribute> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<EcommerceCategoryAttribute>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(EcommerceCategoryAttribute item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(EcommerceCategoryAttribute item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
