using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IChucVuRepository
    {
        IQueryable<ChucVu> ChucVus { get; }

        Task<List<ChucVu>> GetListAsync();

        Task<ChucVu> GetByIdAsync(int Id);

        Task<int> InsertAsync(ChucVu item);

        Task UpdateAsync(ChucVu item);

        Task DeleteAsync(ChucVu item);
    }
}
