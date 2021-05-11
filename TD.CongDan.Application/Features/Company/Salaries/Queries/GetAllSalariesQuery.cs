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

namespace TD.CongDan.Application.Features.Salaries.Queries
{
    public class GetAllSalariesQuery : IRequest<PaginatedResult<SalariesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllSalariesQuery(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllSalariesQuery, PaginatedResult<SalariesResponse>>
    {
        private readonly ISalaryRepository _repository;

        public GetAllQueryHandler(ISalaryRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<SalariesResponse>> Handle(GetAllSalariesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Salary, SalariesResponse>> expression = e => new SalariesResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Code = e.Code,
            };
            var paginatedList = await _repository.Salaries
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}