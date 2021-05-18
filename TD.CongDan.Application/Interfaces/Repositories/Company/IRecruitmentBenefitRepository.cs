using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IRecruitmentBenefitRepository
    {
        IQueryable<RecruitmentBenefit> RecruitmentBenefits { get; }

        Task<List<RecruitmentBenefit>> GetListAsync();

        Task<RecruitmentBenefit> GetByIdAsync(int Id);

        Task<int> InsertAsync(RecruitmentBenefit item);

        Task UpdateAsync(RecruitmentBenefit item);

        Task DeleteAsync(RecruitmentBenefit item);
    }
}
