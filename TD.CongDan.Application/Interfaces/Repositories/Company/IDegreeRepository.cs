using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IDegreeRepository
    {
        IQueryable<Degree> Degrees { get; }

        Task<List<Degree>> GetListAsync();

        Task<Degree> GetByIdAsync(int Id);

        Task<int> InsertAsync(Degree item);

        Task UpdateAsync(Degree item);

        Task DeleteAsync(Degree item);
    }
}
