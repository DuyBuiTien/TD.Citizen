using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IJobTypeRepository
    {
        IQueryable<JobType> JobTypes { get; }

        Task<List<JobType>> GetListAsync();

        Task<JobType> GetByIdAsync(int Id);

        Task<int> InsertAsync(JobType item);

        Task UpdateAsync(JobType item);

        Task DeleteAsync(JobType item);
    }
}
