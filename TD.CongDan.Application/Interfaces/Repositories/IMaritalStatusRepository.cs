using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IMaritalStatusRepository
    {
        IQueryable<MaritalStatus> MaritalStatuses { get; }

        Task<List<MaritalStatus>> GetListAsync();

      
    }
}