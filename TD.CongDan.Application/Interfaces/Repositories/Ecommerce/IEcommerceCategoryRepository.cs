using TD.CongDan.Domain.Entities.Ecommerce;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IEcommerceCategoryRepository
    {
        IQueryable<EcommerceCategory> EcommerceCategories { get; }

        Task<List<EcommerceCategory>> GetListAsync();

        Task<EcommerceCategory> GetByIdAsync(int Id);

        Task<int> InsertAsync(EcommerceCategory item);

        Task UpdateAsync(EcommerceCategory item);

        Task DeleteAsync(EcommerceCategory item);
    }
}