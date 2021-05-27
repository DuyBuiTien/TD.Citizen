using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class PlaceTypeRepository : IPlaceTypeRepository
    {
        private readonly IRepositoryAsync<PlaceType> _repository;
        private readonly IDistributedCache _distributedCache;

        public PlaceTypeRepository(IDistributedCache distributedCache, IRepositoryAsync<PlaceType> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }
        public IQueryable<PlaceType> PlaceTypes => _repository.Entities;

        public async Task DeleteAsync(PlaceType item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<PlaceType> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<PlaceType> GetByNameAsync(string Name)
        {
            return await _repository.Entities.Where(p => p.Name == Name).FirstOrDefaultAsync();
        }

        public async Task<List<PlaceType>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(PlaceType item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(PlaceType item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
