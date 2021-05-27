using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IQuocGiaRepository
    {
        IQueryable<QuocGia> QuocGias { get; }

        Task<List<QuocGia>> GetListAsync();

        Task<QuocGia> GetByIdAsync(int Id);

        Task<int> InsertAsync(QuocGia item);

        Task UpdateAsync(QuocGia item);

        Task DeleteAsync(QuocGia item);
    }
}
