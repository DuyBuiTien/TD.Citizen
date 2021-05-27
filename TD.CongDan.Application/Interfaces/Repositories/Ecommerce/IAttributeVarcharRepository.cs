using TD.CongDan.Domain.Entities.Ecommerce;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IAttributeVarcharRepository
    {
        IQueryable<AttributeVarchar> AttributeVarchars { get; }

        Task<List<AttributeVarchar>> GetListAsync();

        Task<AttributeVarchar> GetByIdAsync(int Id);

        Task<int> InsertAsync(AttributeVarchar item);

        Task UpdateAsync(AttributeVarchar item);

        Task DeleteAsync(AttributeVarchar item);
    }
}