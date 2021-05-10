using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IIndustryRepository
    {
        IQueryable<Industry> Industries { get; }

        Task<List<Industry>> GetListAsync();

        Task<Industry> GetByIdAsync(int Id);

        Task<int> InsertAsync(Industry item);

        Task UpdateAsync(Industry item);

        Task DeleteAsync(Industry item);
    }
}
