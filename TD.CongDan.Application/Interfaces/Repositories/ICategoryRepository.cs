using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        Task<List<Category>> GetListAsync();

        Task<Category> GetByIdAsync(int categoryId);

        Task<int> InsertAsync(Category category);

        Task UpdateAsync(Category category);

        Task DeleteAsync(Category category);
    }
}
