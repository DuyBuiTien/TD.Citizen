using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IIdentityTypeRepository
    {
        IQueryable<IdentityType> IdentityTypes { get; }

        Task<List<IdentityType>> GetListAsync();

      
    }
}