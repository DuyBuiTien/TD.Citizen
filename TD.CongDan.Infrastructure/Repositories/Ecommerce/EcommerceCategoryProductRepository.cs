using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class EcommerceCategoryProductRepository : IEcommerceCategoryProductRepository
    {
        private readonly IRepositoryAsync<EcommerceCategoryProduct> _repository;

        public EcommerceCategoryProductRepository(IRepositoryAsync<EcommerceCategoryProduct> repository)
        {
            _repository = repository;
        }

        public IQueryable<EcommerceCategoryProduct> EcommerceCategoryProducts => _repository.Entities;


        public async Task DeleteAsync(EcommerceCategoryProduct item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<EcommerceCategoryProduct> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<EcommerceCategoryProduct>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(EcommerceCategoryProduct item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(EcommerceCategoryProduct item)
        {
            await _repository.UpdateAsync(item);            
        }
    }
}
