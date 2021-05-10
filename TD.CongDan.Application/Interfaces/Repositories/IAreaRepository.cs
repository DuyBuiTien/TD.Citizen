using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IAreaRepository
    {
        IQueryable<Area> Areas { get; }

        Task<List<Area>> GetListAsync();

        Task<Area> GetByIdAsync(int Id);
        Task<Area> GetByCodeAsync(string code);

        Task<int> InsertAsync(Area item);

        Task UpdateAsync(Area item);

        Task DeleteAsync(Area item);
    }
}
