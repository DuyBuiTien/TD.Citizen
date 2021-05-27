using TD.CongDan.Domain.Entities.Ecommerce;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IAttributeValueRepository
    {
        IQueryable<AttributeValue> AttributeValues { get; }

        Task<List<AttributeValue>> GetListAsync();

        Task<AttributeValue> GetByIdAsync(int Id);

        Task<int> InsertAsync(AttributeValue item);

        Task UpdateAsync(AttributeValue item);

        Task DeleteAsync(AttributeValue item);
    }
}