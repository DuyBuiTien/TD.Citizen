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
using TD.CongDan.Domain.Entities.Covid;

namespace TD.CongDan.Application.Features.QuocGias.Queries
{
    public class GetAllQuocGiasQuery : IRequest<PaginatedResult<QuocGiasResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllQuocGiasQuery(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuocGiasQuery, PaginatedResult<QuocGiasResponse>>
    {
        private readonly IQuocGiaRepository _repository;

        public GetAllQueryHandler(IQuocGiaRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<QuocGiasResponse>> Handle(GetAllQuocGiasQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<QuocGia, QuocGiasResponse>> expression = e => new QuocGiasResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Code = e.Code,
            };
            var paginatedList = await _repository.QuocGias
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}