using TD.CongDan.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Interfaces.Repositories
{
    public interface IAttachmentRepository
    {
        Task<Attachment> InsertAsync(Attachment attachment);
    }
}