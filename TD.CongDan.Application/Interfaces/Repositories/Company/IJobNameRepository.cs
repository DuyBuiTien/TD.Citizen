using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IJobNameRepository
    {
        IQueryable<JobName> JobNames { get; }

        Task<List<JobName>> GetListAsync();

        Task<JobName> GetByIdAsync(int Id);

        Task<int> InsertAsync(JobName item);

        Task UpdateAsync(JobName item);

        Task DeleteAsync(JobName item);
    }
}
