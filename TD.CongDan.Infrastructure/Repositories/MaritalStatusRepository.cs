using TD.CongDan.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class MaritalStatusRepository : IMaritalStatusRepository
    {
        private readonly IRepositoryAsync<MaritalStatus> _repository;

        public MaritalStatusRepository(IDistributedCache distributedCache, IRepositoryAsync<MaritalStatus> repository)
        {
            _repository = repository;
        }

        public IQueryable<MaritalStatus> MaritalStatuses => _repository.Entities;

        

        public async Task<List<MaritalStatus>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

     
    }
}