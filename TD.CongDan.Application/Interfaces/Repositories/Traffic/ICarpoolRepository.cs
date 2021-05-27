using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface ICarpoolRepository
    {
        IQueryable<Carpool> Carpools { get; }

        Task<List<Carpool>> GetListAsync();

        Task<Carpool> GetByIdAsync(int Id);

        Task<int> InsertAsync(Carpool item);

        Task UpdateAsync(Carpool item);

        Task DeleteAsync(Carpool item);
    }
}
