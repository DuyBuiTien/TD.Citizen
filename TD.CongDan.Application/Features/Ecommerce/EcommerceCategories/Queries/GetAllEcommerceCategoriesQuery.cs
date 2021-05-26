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

namespace TD.CongDan.Application.Features.EcommerceCategories.Queries
{
    public class GetAllEcommerceCategoriesQuery : IRequest<PaginatedResult<EcommerceCategoriesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? Level { get; set; }
        public int? ParentId { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }
        
        public GetAllEcommerceCategoriesQuery(int pageNumber, int pageSize, string keySearch, string orderBy, int? level, int? parentId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
            Level = level;
            ParentId = parentId;
        }
    }

    public class GGetAllAreasQueryHandler : IRequestHandler<GetAllEcommerceCategoriesQuery, PaginatedResult<EcommerceCategoriesResponse>>
    {
        private readonly IEcommerceCategoryRepository _repository;

        public GGetAllAreasQueryHandler(IEcommerceCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<EcommerceCategoriesResponse>> Handle(GetAllEcommerceCategoriesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<EcommerceCategory, EcommerceCategoriesResponse>> expression = e => new EcommerceCategoriesResponse
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


            
            var paginatedList = await _repository.EcommerceCategories
                .FilterParentId(request.ParentId)
                .FilterLevel(request.Level)
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }


    }
}
