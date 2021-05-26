using TD.CongDan.Application.Extensions;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.Libs.Results;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Application.Features.Brands.Queries.GetAll
{
    public class GetAllBrandsQuery : IRequest<PaginatedResult<GetAllBrandsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllBrandsQuery(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
        }
    }

    public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandsQuery, PaginatedResult<GetAllBrandsResponse>>
    {
        private readonly IBrandRepository _repository;

        public GetAllBrandQueryHandler(IBrandRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllBrandsResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Brand, GetAllBrandsResponse>> expression = e => new GetAllBrandsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Code = e.Code,
            };
            var paginatedList = await _repository.Brands
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}