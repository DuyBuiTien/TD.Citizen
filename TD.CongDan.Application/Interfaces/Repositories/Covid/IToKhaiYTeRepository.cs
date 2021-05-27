using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IToKhaiYTeRepository
    {
        IQueryable<ToKhaiYTe> ToKhaiYTes { get; }

        Task<List<ToKhaiYTe>> GetListAsync();

        Task<ToKhaiYTe> GetByIdAsync(int Id);

        Task<int> InsertAsync(ToKhaiYTe item);

        Task UpdateAsync(ToKhaiYTe item);

        Task DeleteAsync(ToKhaiYTe item);
    }
}
