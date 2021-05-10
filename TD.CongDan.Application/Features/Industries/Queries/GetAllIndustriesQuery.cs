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

namespace TD.CongDan.Application.Features.Industries.Queries
{
    public class GetAllIndustriesQuery : IRequest<PaginatedResult<IndustriesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllIndustriesQuery(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllIndustriesQuery, PaginatedResult<IndustriesResponse>>
    {
        private readonly IIndustryRepository _repository;

        public GetAllQueryHandler(IIndustryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<IndustriesResponse>> Handle(GetAllIndustriesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Industry, IndustriesResponse>> expression = e => new IndustriesResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Code = e.Code,
            };
            var paginatedList = await _repository.Industries
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}