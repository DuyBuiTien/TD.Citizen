using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IToKhaiYTeBenhNenRepository
    {
        IQueryable<ToKhaiYTeBenhNen> ToKhaiYTeBenhNens { get; }

        Task<List<ToKhaiYTeBenhNen>> GetListAsync();

        Task<ToKhaiYTeBenhNen> GetByIdAsync(int Id);

        Task<int> InsertAsync(ToKhaiYTeBenhNen item);

        Task UpdateAsync(ToKhaiYTeBenhNen item);

        Task DeleteAsync(ToKhaiYTeBenhNen item);
    }
}
