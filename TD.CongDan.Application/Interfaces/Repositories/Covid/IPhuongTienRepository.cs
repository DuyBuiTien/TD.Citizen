using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IPhuongTienRepository
    {
        IQueryable<PhuongTien> PhuongTiens { get; }

        Task<List<PhuongTien>> GetListAsync();

        Task<PhuongTien> GetByIdAsync(int Id);

        Task<int> InsertAsync(PhuongTien item);

        Task UpdateAsync(PhuongTien item);

        Task DeleteAsync(PhuongTien item);
    }
}
