using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IJobApplicationRepository
    {
        IQueryable<JobApplication> JobApplications { get; }

        Task<List<JobApplication>> GetListAsync();

        Task<JobApplication> GetByIdAsync(int Id);
        Task<JobApplication> GetByUsernameAsync(string UserName);

        Task<int> InsertAsync(JobApplication item);

        Task UpdateAsync(JobApplication item);

        Task DeleteAsync(JobApplication item);
    }
}
