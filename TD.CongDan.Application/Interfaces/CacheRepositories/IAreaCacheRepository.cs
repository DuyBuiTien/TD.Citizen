using System.Collections.Generic;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Interfaces.CacheRepositories
{
    public interface IAreaCacheRepository
    {
        Task<List<Area>> GetCachedListAsync();

        Task<Area> GetByIdAsync(int Id);
    }
}