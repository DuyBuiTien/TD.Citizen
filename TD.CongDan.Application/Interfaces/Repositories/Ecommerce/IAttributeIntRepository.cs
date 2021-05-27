using TD.CongDan.Domain.Entities.Ecommerce;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IAttributeIntRepository
    {
        IQueryable<AttributeInt> AttributeInts { get; }

        Task<List<AttributeInt>> GetListAsync();

        Task<AttributeInt> GetByIdAsync(int Id);

        Task<int> InsertAsync(AttributeInt item);

        Task UpdateAsync(AttributeInt item);

        Task DeleteAsync(AttributeInt item);
    }
}