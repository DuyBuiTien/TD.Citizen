using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IJobAgeRepository
    {
        IQueryable<JobAge> JobAges { get; }

        Task<List<JobAge>> GetListAsync();

        Task<JobAge> GetByIdAsync(int Id);

        Task<int> InsertAsync(JobAge item);

        Task UpdateAsync(JobAge item);

        Task DeleteAsync(JobAge item);
    }
}
