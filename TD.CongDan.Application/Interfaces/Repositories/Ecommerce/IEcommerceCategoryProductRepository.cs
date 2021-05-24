using TD.CongDan.Domain.Entities.Ecommerce;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IEcommerceCategoryProductRepository
    {
        IQueryable<EcommerceCategoryProduct> EcommerceCategoryProducts { get; }

        Task<List<EcommerceCategoryProduct>> GetListAsync();

        Task<EcommerceCategoryProduct> GetByIdAsync(int Id);

        Task<int> InsertAsync(EcommerceCategoryProduct item);

        Task UpdateAsync(EcommerceCategoryProduct item);

        Task DeleteAsync(EcommerceCategoryProduct item);
    }
}