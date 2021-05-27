using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class EcommerceCategoryRepository : IEcommerceCategoryRepository
    {
        private readonly IRepositoryAsync<EcommerceCategory> _repository;

        public EcommerceCategoryRepository(IRepositoryAsync<EcommerceCategory> repository)
        {
            _repository = repository;
        }

        public IQueryable<EcommerceCategory> EcommerceCategories => _repository.Entities;


        public async Task DeleteAsync(EcommerceCategory item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<EcommerceCategory> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<EcommerceCategory>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(EcommerceCategory item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(EcommerceCategory item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
