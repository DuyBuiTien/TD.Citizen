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

namespace TD.CongDan.Application.Features.Benefits.Queries
{
    public class GetAllBenefitsQuery : IRequest<PaginatedResult<BenefitsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllBenefitsQuery(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllBenefitsQuery, PaginatedResult<BenefitsResponse>>
    {
        private readonly IBenefitRepository _repository;

        public GetAllQueryHandler(IBenefitRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<BenefitsResponse>> Handle(GetAllBenefitsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Benefit, BenefitsResponse>> expression = e => new BenefitsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Code = e.Code,
                Icon = e.Icon,
            };
            var paginatedList = await _repository.Benefits
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}