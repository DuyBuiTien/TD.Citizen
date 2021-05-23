using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IGenderRepository
    {
        IQueryable<Gender> Genders { get; }

        Task<List<Gender>> GetListAsync();

        Task<Gender> GetByIdAsync(int productId);

        Task<int> InsertAsync(Gender product);

        Task UpdateAsync(Gender product);

        Task DeleteAsync(Gender product);
    }
}