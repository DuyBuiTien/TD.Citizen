using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IReligionRepository
    {
        IQueryable<Religion> Religions { get; }
        Task<List<Religion>> GetListAsync();
    }
}