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

namespace TD.CongDan.Application.Features.JobPositions.Queries
{
    public class GetAllJobPositionsQuery : IRequest<PaginatedResult<JobPositionsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllJobPositionsQuery(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllJobPositionsQuery, PaginatedResult<JobPositionsResponse>>
    {
        private readonly IJobPositionRepository _repository;

        public GetAllQueryHandler(IJobPositionRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<JobPositionsResponse>> Handle(GetAllJobPositionsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<JobPosition, JobPositionsResponse>> expression = e => new JobPositionsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Code = e.Code,
            };
            var paginatedList = await _repository.JobPositions
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}