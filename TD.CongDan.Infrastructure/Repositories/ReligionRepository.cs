using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class ReligionRepository : IReligionRepository
    {
        private readonly IRepositoryAsync<Religion> _repository;

        public ReligionRepository(IDistributedCache distributedCache, IRepositoryAsync<Religion> repository)
        {
            _repository = repository;
        }

        public IQueryable<Religion> Religions => _repository.Entities;

        

        public async Task<List<Religion>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

     
    }
}