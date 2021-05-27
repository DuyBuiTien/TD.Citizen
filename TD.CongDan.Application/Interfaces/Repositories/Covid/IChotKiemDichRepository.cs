using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IChotKiemDichRepository
    {
        IQueryable<ChotKiemDich> ChotKiemDichs { get; }

        Task<List<ChotKiemDich>> GetListAsync();

        Task<ChotKiemDich> GetByIdAsync(int Id);

        Task<int> InsertAsync(ChotKiemDich item);

        Task UpdateAsync(ChotKiemDich item);

        Task DeleteAsync(ChotKiemDich item);
    }
}
