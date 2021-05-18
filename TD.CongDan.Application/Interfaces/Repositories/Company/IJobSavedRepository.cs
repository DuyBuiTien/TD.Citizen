using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IJobSavedRepository
    {
        IQueryable<JobSaved> JobSaveds { get; }

        Task<List<JobSaved>> GetListAsync();

        Task<JobSaved> GetByIdAsync(string UserName, int RecruitmentId);

        Task<int> InsertAsync(JobSaved item);

        Task UpdateAsync(JobSaved item);

        Task DeleteAsync(JobSaved item);
    }
}
