using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IToKhaiYTeTrieuChungRepository
    {
        IQueryable<ToKhaiYTeTrieuChung> ToKhaiYTeTrieuChungs { get; }

        Task<List<ToKhaiYTeTrieuChung>> GetListAsync();

        Task<ToKhaiYTeTrieuChung> GetByIdAsync(int Id);

        Task<int> InsertAsync(ToKhaiYTeTrieuChung item);

        Task UpdateAsync(ToKhaiYTeTrieuChung item);

        Task DeleteAsync(ToKhaiYTeTrieuChung item);
    }
}
