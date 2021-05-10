using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IPlaceRepository
    {
        IQueryable<Place> Places { get; }

        Task<List<Place>> GetListAsync();

        Task<Place> GetByIdAsync(int Id);

        Task<int> InsertAsync(Place item);

        Task UpdateAsync(Place item);

        Task DeleteAsync(Place item);
    }
}
