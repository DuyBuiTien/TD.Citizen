using TD.CongDan.Domain.Entities.Ecommerce;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IAttributeRepository
    {
        IQueryable<Attribute> Attributes { get; }

        Task<List<Attribute>> GetListAsync();

        Task<Attribute> GetByIdAsync(int Id);

        Task<int> InsertAsync(Attribute item);

        Task UpdateAsync(Attribute item);

        Task DeleteAsync(Attribute item);
    }
}