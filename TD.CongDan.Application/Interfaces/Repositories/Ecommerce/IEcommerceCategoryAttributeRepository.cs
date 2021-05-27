using TD.CongDan.Domain.Entities.Ecommerce;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IEcommerceCategoryAttributeRepository
    {
        IQueryable<EcommerceCategoryAttribute> EcommerceCategoryAttributes { get; }

        Task<List<EcommerceCategoryAttribute>> GetListAsync();

        Task<EcommerceCategoryAttribute> GetByIdAsync(int Id);

        Task<int> InsertAsync(EcommerceCategoryAttribute item);

        Task UpdateAsync(EcommerceCategoryAttribute item);

        Task DeleteAsync(EcommerceCategoryAttribute item);
    }
}