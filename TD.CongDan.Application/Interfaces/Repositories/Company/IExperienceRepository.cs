using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IExperienceRepository
    {
        IQueryable<Experience> Experiences { get; }

        Task<List<Experience>> GetListAsync();

        Task<Experience> GetByIdAsync(int Id);

        Task<int> InsertAsync(Experience item);

        Task UpdateAsync(Experience item);

        Task DeleteAsync(Experience item);
    }
}
