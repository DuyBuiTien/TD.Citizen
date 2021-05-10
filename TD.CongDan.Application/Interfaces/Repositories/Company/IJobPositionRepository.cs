using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IJobPositionRepository
    {
        IQueryable<JobPosition> JobPositions { get; }

        Task<List<JobPosition>> GetListAsync();

        Task<JobPosition> GetByIdAsync(int Id);

        Task<int> InsertAsync(JobPosition item);

        Task UpdateAsync(JobPosition item);

        Task DeleteAsync(JobPosition item);
    }
}
