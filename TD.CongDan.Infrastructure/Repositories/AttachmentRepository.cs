using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Application.CacheKeys;
using Microsoft.EntityFrameworkCore;

namespace TD.CongDan.Infrastructure.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {

        private readonly IRepositoryAsync<Attachment> _repository;
        private readonly IDistributedCache _distributedCache;

        public AttachmentRepository(IDistributedCache distributedCache, IRepositoryAsync<Attachment> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }



        public async Task<Attachment> InsertAsync(Attachment attachment)
        {
            await _repository.AddAsync(attachment);
            return attachment;
        }

  
    }
}
