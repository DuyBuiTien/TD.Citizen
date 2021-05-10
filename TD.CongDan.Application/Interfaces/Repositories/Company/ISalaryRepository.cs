using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface ISalaryRepository
    {
        IQueryable<Salary> Salaries { get; }

        Task<List<Salary>> GetListAsync();

        Task<Salary> GetByIdAsync(int Id);

        Task<int> InsertAsync(Salary item);

        Task UpdateAsync(Salary item);

        Task DeleteAsync(Salary item);
    }
}
