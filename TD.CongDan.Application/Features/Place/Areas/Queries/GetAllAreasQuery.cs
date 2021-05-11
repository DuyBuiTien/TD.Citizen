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

namespace TD.CongDan.Application.Features.Are.Queries
{
    public class GetAllAreaQuery : IRequest<PaginatedResult<AreasResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string ParentCode { get; set; }
        public string Type { get; set; }
        public int? Level { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }
        
        public GetAllAreaQuery(int pageNumber, int pageSize, string parentCode, string type, string keySearch, string orderBy, int? level)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            ParentCode = parentCode;
            Type = type;
            KeySearch = keySearch;
            OrderBy = orderBy;
            Level = level;
        }
    }

    public class GGetAllAreasQueryHandler : IRequestHandler<GetAllAreaQuery, PaginatedResult<AreasResponse>>
    {
        private readonly IAreaRepository _repository;

        public GGetAllAreasQueryHandler(IAreaRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<AreasResponse>> Handle(GetAllAreaQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Area, AreasResponse>> expression = e => new AreasResponse
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.Code,
                ParentCode = e.ParentCode,
                Slug = e.Slug,
                Type = e.Type,
                NameWithType = e.NameWithType,
                Path = e.Path,
                PathWithType = e.PathWithType,
                Description = e.Description,
                Level = e.Level
            };


            var paginatedList = await _repository.Areas
                .FilterAreaParentCode(request.ParentCode)
                .FilterAreaType(request.Type)
                .FilterAreaLevel(request.Level)
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }


    }
}
