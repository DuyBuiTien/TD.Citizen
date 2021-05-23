using TD.CongDan.Domain.Entities.Ecommerce;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Interfaces.CacheRepositories
{
    public interface IProductCacheRepository
    {
        Task<List<Product>> GetCachedListAsync();

        Task<Product> GetByIdAsync(int brandId);
    }
}