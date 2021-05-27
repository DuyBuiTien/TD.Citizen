using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class TrafficTicketRepository : ITrafficTicketRepository
    {
        private readonly IRepositoryAsync<TrafficTicket> _repository;
        private readonly IDistributedCache _distributedCache;

        public TrafficTicketRepository(IDistributedCache distributedCache, IRepositoryAsync<TrafficTicket> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }
        public IQueryable<TrafficTicket> TrafficTickets => _repository.Entities;

        public async Task DeleteAsync(TrafficTicket item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<TrafficTicket> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }



        public async Task<List<TrafficTicket>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(TrafficTicket item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(TrafficTicket item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
