using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IVehicleTypeRepository
    {
        IQueryable<VehicleType> VehicleTypes { get; }

        Task<List<VehicleType>> GetListAsync();

        Task<VehicleType> GetByIdAsync(int Id);

        Task<int> InsertAsync(VehicleType item);

        Task UpdateAsync(VehicleType item);

        Task DeleteAsync(VehicleType item);
    }
}
