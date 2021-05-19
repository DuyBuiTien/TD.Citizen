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
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        private readonly IRepositoryAsync<VehicleType> _repository;
        private readonly IDistributedCache _distributedCache;

        public VehicleTypeRepository(IDistributedCache distributedCache, IRepositoryAsync<VehicleType> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }
        public IQueryable<VehicleType> VehicleTypes => _repository.Entities;

        public async Task DeleteAsync(VehicleType item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<VehicleType> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<List<VehicleType>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(VehicleType item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(VehicleType item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
