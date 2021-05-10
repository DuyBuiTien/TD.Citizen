using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IPlaceTypeRepository
    {
        IQueryable<PlaceType> PlaceTypes { get; }

        Task<List<PlaceType>> GetListAsync();

        Task<PlaceType> GetByIdAsync(int Id);

        Task<PlaceType> GetByNameAsync(string name);


        Task<int> InsertAsync(PlaceType item);

        Task UpdateAsync(PlaceType item);

        Task DeleteAsync(PlaceType item);
    }
}
