using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IJobAppliedRepository
    {
        IQueryable<JobApplied> JobApplieds { get; }

        Task<List<JobApplied>> GetListAsync();

        Task<JobApplied> GetByIdAsync(string UserName, int RecruitmentId);

        Task<int> InsertAsync(JobApplied item);

        Task UpdateAsync(JobApplied item);

        Task DeleteAsync(JobApplied item);
    }
}
