using TD.CongDan.Domain.Entities.Ecommerce;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }

        Task<List<Product>> GetListAsync();

        Task<Product> GetByIdAsync(int productId);

        Task<int> InsertAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Product product);
    }
}