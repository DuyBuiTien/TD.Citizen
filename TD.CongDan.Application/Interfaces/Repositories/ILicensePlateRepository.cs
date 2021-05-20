using TD.CongDan.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface ILicensePlateRepository
    {
        IQueryable<LicensePlate> LicensePlates { get; }

        Task<List<LicensePlate>> GetListAsync();

        Task<LicensePlate> GetByIdAsync(int Id);


        Task<int> InsertAsync(LicensePlate item);

        Task UpdateAsync(LicensePlate item);

        Task DeleteAsync(LicensePlate item);
    }
}