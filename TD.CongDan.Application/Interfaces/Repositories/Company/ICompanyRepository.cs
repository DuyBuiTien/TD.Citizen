using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        IQueryable<Company> Companies { get; }

        Task<List<Company>> GetListAsync();

        Task<Company> GetByIdAsync(int Id);

        Task<Company> GetByUserNameAsync(string UserName);

        Task<int> InsertAsync(Company item);

        Task UpdateAsync(Company item);

        Task DeleteAsync(Company item);
    }
}
