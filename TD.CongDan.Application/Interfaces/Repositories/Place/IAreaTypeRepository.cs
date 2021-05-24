using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IAreaTypeRepository
    {
        IQueryable<AreaType> AreaTypes { get; }

        Task<List<AreaType>> GetListAsync();

        Task<AreaType> GetByIdAsync(int Id);
        Task<AreaType> GetByCodeAsync(string code);

        Task<int> InsertAsync(AreaType item);

        Task UpdateAsync(AreaType item);

        Task DeleteAsync(AreaType item);
    }
}
