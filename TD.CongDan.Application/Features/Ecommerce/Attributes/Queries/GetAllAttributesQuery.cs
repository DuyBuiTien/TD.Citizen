using TD.Libs.Results;
using MediatR;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Extensions;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities.Ecommerce;
using System;

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
            Expression<Func<Domain.Entities.Ecommerce.Attribute, AttributesResponse>> expression = e => new AttributesResponse
            {
                Id = e.Id,
                Code = e.Code,
                DisplayName = e.DisplayName,
                Description = e.Description,
                IsVisibleOnFront = e.IsVisibleOnFront,
                IsRequired = e.IsRequired,
                IsFilterable = e.IsFilterable,
                IsSearchable = e.IsSearchable,
                IsSellerEditable = e.IsSellerEditable,
                DefaultValue = e.DefaultValue,
                FrontendInput = e.FrontendInput,
                DataType = e.DataType,
                InputType = e.InputType

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
