using System.Collections.Generic;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Interfaces.CacheRepositories
{
    public interface ICategoryCacheRepository
    {
        Task<List<Category>> GetCachedListAsync();

        Task<Category> GetByIdAsync(int categoryId);
    }
}