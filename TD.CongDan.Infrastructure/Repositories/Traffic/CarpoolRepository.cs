using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class CarpoolRepository : ICarpoolRepository
    {
        private readonly IRepositoryAsync<Carpool> _repository;
        private readonly IDistributedCache _distributedCache;

        public CarpoolRepository(IDistributedCache distributedCache, IRepositoryAsync<Carpool> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }
        public IQueryable<Carpool> Carpools => _repository.Entities;

        public async Task DeleteAsync(Carpool item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<Carpool> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).Include(x=>x.PlaceArrival).Include(x=>x.PlaceDeparture).Include(x=>x.VehicleType).FirstOrDefaultAsync();
        }
        public async Task<List<Carpool>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Carpool item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(Carpool item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
