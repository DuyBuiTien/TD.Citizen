using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IDonViCongTacRepository
    {
        IQueryable<DonViCongTac> DonViCongTacs { get; }

        Task<List<DonViCongTac>> GetListAsync();

        Task<DonViCongTac> GetByIdAsync(int Id);

        Task<int> InsertAsync(DonViCongTac item);

        Task UpdateAsync(DonViCongTac item);

        Task DeleteAsync(DonViCongTac item);
    }
}
