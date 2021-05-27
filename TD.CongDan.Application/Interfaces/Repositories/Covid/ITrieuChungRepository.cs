using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface ITrieuChungRepository
    {
        IQueryable<TrieuChung> TrieuChungs { get; }

        Task<List<TrieuChung>> GetListAsync();

        Task<TrieuChung> GetByIdAsync(int Id);

        Task<int> InsertAsync(TrieuChung item);

        Task UpdateAsync(TrieuChung item);

        Task DeleteAsync(TrieuChung item);
    }
}
