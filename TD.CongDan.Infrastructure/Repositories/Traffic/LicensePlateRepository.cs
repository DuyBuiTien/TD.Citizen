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
    public class LicensePlateRepository : ILicensePlateRepository
    {
        private readonly IRepositoryAsync<LicensePlate> _repository;
        private readonly IDistributedCache _distributedCache;

        public LicensePlateRepository(IDistributedCache distributedCache, IRepositoryAsync<LicensePlate> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }
        public IQueryable<LicensePlate> LicensePlates => _repository.Entities;

        public async Task DeleteAsync(LicensePlate item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<LicensePlate> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }


        public async Task<List<LicensePlate>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(LicensePlate item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(LicensePlate item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
