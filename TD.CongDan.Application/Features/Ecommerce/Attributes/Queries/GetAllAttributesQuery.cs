using TD.Libs.Results;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Extensions;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Application.Features.Attributes.Queries
{
    public class GetAllAttributesQuery : IRequest<PaginatedResult<AttributesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
       
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }
        
        public GetAllAttributesQuery(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
           
        }
    }

    public class GGetAllAreasQueryHandler : IRequestHandler<GetAllAttributesQuery, PaginatedResult<AttributesResponse>>
    {
        private readonly IAttributeRepository _repository;

        public GGetAllAreasQueryHandler(IAttributeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<AttributesResponse>> Handle(GetAllAttributesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<EcommerceCategory, AttributesResponse>> expression = e => new AttributesResponse
            {
                Id = e.Id,
                Name = e.Name,
                ParentId = e.ParentId,
                Description = e.Description,
                Level = e.Level,
                Slug = e.Slug,
                MetaTitle = e.MetaTitle,
                Position = e.Position,
                MetaDescription = e.MetaDescription,
                Icon = e.Icon, 
                Image = e.Image,
                Tags = e.Tags
            };


            
            var paginatedList = await _repository.Attributes
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }


    }
}
