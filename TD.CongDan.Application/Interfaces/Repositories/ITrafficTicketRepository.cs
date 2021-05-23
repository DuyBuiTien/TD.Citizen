using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface ITrafficTicketRepository
    {
        IQueryable<TrafficTicket> TrafficTickets { get; }

        Task<List<TrafficTicket>> GetListAsync();

        Task<TrafficTicket> GetByIdAsync(int Id);

        Task<int> InsertAsync(TrafficTicket item);

        Task UpdateAsync(TrafficTicket item);

        Task DeleteAsync(TrafficTicket item);


    }
}