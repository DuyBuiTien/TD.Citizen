using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IBenhNenRepository
    {
        IQueryable<BenhNen> BenhNens { get; }

        Task<List<BenhNen>> GetListAsync();

        Task<BenhNen> GetByIdAsync(int Id);

        Task<int> InsertAsync(BenhNen item);

        Task UpdateAsync(BenhNen item);

        Task DeleteAsync(BenhNen item);
    }
}
