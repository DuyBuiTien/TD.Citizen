using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Other;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IAppConfigRepository
    {
        IQueryable<AppConfig> AppConfigs { get; }

        Task<List<AppConfig>> GetListAsync();

        Task<AppConfig> GetByIdAsync(string Key);

        Task<int> InsertAsync(AppConfig item);

        Task UpdateAsync(AppConfig item);

        Task DeleteAsync(AppConfig item);
    }
}