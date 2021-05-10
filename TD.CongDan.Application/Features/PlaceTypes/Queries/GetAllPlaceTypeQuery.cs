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

namespace TD.CongDan.Application.Features.PlaceTypes.Queries
{
    public class GetAllPlaceTypeQuery : IRequest<PaginatedResult<PlaceTypesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string ParentCode { get; set; }
        public string Type { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }
        public int CategoryId { get; set; }

        public GetAllPlaceTypeQuery(int pageNumber, int pageSize, string parentCode, string type, string keySearch, string orderBy, int categoryId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            ParentCode = parentCode;
            Type = type;
            KeySearch = keySearch;
            OrderBy = orderBy;
            CategoryId = categoryId;
        }
    }

    public class GGetAllQueryHandler : IRequestHandler<GetAllPlaceTypeQuery, PaginatedResult<PlaceTypesResponse>>
    {
        private readonly IPlaceTypeRepository _repository;

        public GGetAllQueryHandler(IPlaceTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<PlaceTypesResponse>> Handle(GetAllPlaceTypeQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<PlaceType, PlaceTypesResponse>> expression = e => new PlaceTypesResponse
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.Code,
                Icon = e.Icon,
                Image = e.Image,
                CoverImage = e.CoverImage,
                Description = e.Description,
                CategoryId = e.CategoryId,
                Category = e.Category.Name
            };


            var paginatedList = await _repository.PlaceTypes
                .FilterByCategoryId(request.CategoryId)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }


    }
}
