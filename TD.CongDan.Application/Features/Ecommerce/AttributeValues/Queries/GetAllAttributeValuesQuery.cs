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

namespace TD.CongDan.Application.Features.AttributeValues.Queries
{
    public class GetAllAttributeValuesQuery : IRequest<PaginatedResult<AttributevaluesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
       
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }
        public int? AttributeId { get;set }
        
        public GetAllAttributeValuesQuery(int pageNumber, int pageSize, string keySearch, string orderBy, int? attributeId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
            AttributeId = attributeId;
        }
    }

    public class GGetAllAreasQueryHandler : IRequestHandler<GetAllAttributeValuesQuery, PaginatedResult<AttributevaluesResponse>>
    {
        private readonly IAttributeValueRepository _repository;

        public GGetAllAreasQueryHandler(IAttributeValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<AttributevaluesResponse>> Handle(GetAllAttributeValuesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<AttributeValue, AttributevaluesResponse>> expression = e => new AttributevaluesResponse
            {
                Id = e.Id,
                Value = e.Value,
                AttributeId = e.AttributeId,
                Position = e.Position,
                IsDefault = e.IsDefault,
                Status = e.Status,
               
            };


            
            var paginatedList = await _repository.AttributeValues
                .FilterByAttributeId(request.AttributeId)
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }


    }
}
