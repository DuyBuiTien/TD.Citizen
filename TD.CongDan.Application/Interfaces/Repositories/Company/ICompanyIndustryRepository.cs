using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface ICompanyIndustryRepository
    {
        IQueryable<CompanyIndustry> CompanyIndustries { get; }

        Task<List<CompanyIndustry>> GetListAsync();

        Task<CompanyIndustry> GetByIdAsync(int Id);

        Task<int> InsertAsync(CompanyIndustry item);

        Task UpdateAsync(CompanyIndustry item);

        Task DeleteAsync(CompanyIndustry item);
    }
}
