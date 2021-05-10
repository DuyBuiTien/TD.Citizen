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
    public class PlaceRepository : IPlaceRepository
    {
        private readonly IRepositoryAsync<Place> _repository;
        private readonly IDistributedCache _distributedCache;

        public PlaceRepository(IDistributedCache distributedCache, IRepositoryAsync<Place> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }
        public IQueryable<Place> Places => _repository.Entities;

        public async Task DeleteAsync(Place item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<Place> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Place>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Place item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(Place item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
