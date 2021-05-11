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

namespace TD.CongDan.Application.Features.JobAges.Queries
{
    public class GetAllJobAgesQuery : IRequest<PaginatedResult<JobAgesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllJobAgesQuery(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllJobAgesQuery, PaginatedResult<JobAgesResponse>>
    {
        private readonly IJobAgeRepository _repository;

        public GetAllQueryHandler(IJobAgeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<JobAgesResponse>> Handle(GetAllJobAgesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<JobAge, JobAgesResponse>> expression = e => new JobAgesResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Code = e.Code,
            };
            var paginatedList = await _repository.JobAges
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}