using TD.CongDan.Domain.Entities.Ecommerce;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IAttributeDatetimeRepository
    {
        IQueryable<AttributeDatetime> AttributeDatetimes { get; }

        Task<List<AttributeDatetime>> GetListAsync();

        Task<AttributeDatetime> GetByIdAsync(int Id);

        Task<int> InsertAsync(AttributeDatetime item);

        Task UpdateAsync(AttributeDatetime item);

        Task DeleteAsync(AttributeDatetime item);
    }
}