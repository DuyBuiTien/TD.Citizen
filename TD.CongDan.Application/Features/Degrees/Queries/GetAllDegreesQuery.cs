using TD.CongDan.Application.Extensions;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.Libs.Results;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Features.Degrees.Queries
{
    public class GetAllDegreesQuery : IRequest<PaginatedResult<DegreesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllDegreesQuery(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllDegreesQuery, PaginatedResult<DegreesResponse>>
    {
        private readonly IDegreeRepository _repository;

        public GetAllQueryHandler(IDegreeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<DegreesResponse>> Handle(GetAllDegreesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Degree, DegreesResponse>> expression = e => new DegreesResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Code = e.Code,
            };
            var paginatedList = await _repository.Degrees
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}