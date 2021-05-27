using TD.CongDan.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly IRepositoryAsync<Gender> _repository;

        public GenderRepository(IDistributedCache distributedCache, IRepositoryAsync<Gender> repository)
        {
            _repository = repository;
        }

        public IQueryable<Gender> Genders => _repository.Entities;

        public async Task DeleteAsync(Gender product)
        {
            await _repository.DeleteAsync(product);

        }

        public async Task<Gender> GetByIdAsync(int productId)
        {
            return await _repository.Entities.Where(p => p.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<List<Gender>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Gender product)
        {
            await _repository.AddAsync(product);
            return product.Id;
        }

        public async Task UpdateAsync(Gender product)
        {
            await _repository.UpdateAsync(product);
        }
    }
}