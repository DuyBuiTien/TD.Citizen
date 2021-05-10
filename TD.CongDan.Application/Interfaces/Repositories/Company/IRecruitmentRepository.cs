using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IRecruitmentRepository
    {
        IQueryable<Recruitment> Recruitments { get; }

        Task<List<Recruitment>> GetListAsync();

        Task<Recruitment> GetByIdAsync(int Id);

        Task<int> InsertAsync(Recruitment item);

        Task UpdateAsync(Recruitment item);

        Task DeleteAsync(Recruitment item);
    }
}
