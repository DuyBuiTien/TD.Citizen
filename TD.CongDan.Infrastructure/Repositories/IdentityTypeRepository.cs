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
    public class IdentityTypeRepository : IIdentityTypeRepository
    {
        private readonly IRepositoryAsync<IdentityType> _repository;

        public IdentityTypeRepository(IDistributedCache distributedCache, IRepositoryAsync<IdentityType> repository)
        {
            _repository = repository;
        }

        public IQueryable<IdentityType> IdentityTypes => _repository.Entities;

        

        public async Task<List<IdentityType>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

     
    }
}