using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IBenefitRepository
    {
        IQueryable<Benefit> Benefits { get; }

        Task<List<Benefit>> GetListAsync();

        Task<Benefit> GetByIdAsync(int Id);

        Task<int> InsertAsync(Benefit item);

        Task UpdateAsync(Benefit item);

        Task DeleteAsync(Benefit item);
    }
}
