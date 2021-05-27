using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface INguoiKhaiBaoRepository
    {
        IQueryable<NguoiKhaiBao> NguoiKhaiBaos { get; }

        Task<List<NguoiKhaiBao>> GetListAsync();

        Task<NguoiKhaiBao> GetByIdAsync(int Id);

        Task<int> InsertAsync(NguoiKhaiBao item);

        Task UpdateAsync(NguoiKhaiBao item);

        Task DeleteAsync(NguoiKhaiBao item);
    }
}
