using TD.CongDan.Domain.Entities.Ecommerce;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IAttributeDecimalRepository
    {
        IQueryable<AttributeDecimal> AttributeDecimals { get; }

        Task<List<AttributeDecimal>> GetListAsync();

        Task<AttributeDecimal> GetByIdAsync(int Id);

        Task<int> InsertAsync(AttributeDecimal item);

        Task UpdateAsync(AttributeDecimal item);

        Task DeleteAsync(AttributeDecimal item);
    }
}