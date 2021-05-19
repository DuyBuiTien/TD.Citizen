using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Other;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class AppConfigRepository : IAppConfigRepository
    {
        private readonly IRepositoryAsync<AppConfig> _repository;

        public AppConfigRepository(IDistributedCache distributedCache, IRepositoryAsync<AppConfig> repository)
        {
            _repository = repository;
        }

        public IQueryable<AppConfig> AppConfigs => _repository.Entities;

        public async Task DeleteAsync(AppConfig product)
        {
            await _repository.DeleteAsync(product);

        }

        public async Task<AppConfig> GetByIdAsync(string Key)
        {
            return await _repository.Entities.Where(p => p.Key == Key).FirstOrDefaultAsync();
        }

        public async Task<List<AppConfig>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(AppConfig product)
        {
            await _repository.AddAsync(product);
            return product.Id;
        }

        public async Task UpdateAsync(AppConfig product)
        {
            await _repository.UpdateAsync(product);
        }
    }
}