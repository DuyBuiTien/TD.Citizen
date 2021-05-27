using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class TrieuChungRepository : ITrieuChungRepository
    {
        private readonly IRepositoryAsync<TrieuChung> _repository;

        public TrieuChungRepository(IRepositoryAsync<TrieuChung> repository)
        {
            _repository = repository;
        }

        public IQueryable<TrieuChung> TrieuChungs => _repository.Entities;


        public async Task DeleteAsync(TrieuChung item)
        {
            await _repository.DeleteAsync(item);
        }

        public async Task<TrieuChung> GetByIdAsync(int Id)
        {
            return await _repository.Entities.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<TrieuChung>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(TrieuChung item)
        {
            await _repository.AddAsync(item);
            return item.Id;
        }

        public async Task UpdateAsync(TrieuChung item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
