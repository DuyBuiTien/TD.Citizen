using TD.CongDan.Domain.Entities.Ecommerce;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IAttributeTextRepository
    {
        IQueryable<AttributeText> AttributeTexts { get; }

        Task<List<AttributeText>> GetListAsync();

        Task<AttributeText> GetByIdAsync(int Id);

        Task<int> InsertAsync(AttributeText item);

        Task UpdateAsync(AttributeText item);

        Task DeleteAsync(AttributeText item);
    }
}