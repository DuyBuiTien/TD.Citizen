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

namespace TD.CongDan.Application.Features.Experiences.Queries
{
    public class GetAllExperiencesQuery : IRequest<PaginatedResult<ExperiencesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllExperiencesQuery(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllExperiencesQuery, PaginatedResult<ExperiencesResponse>>
    {
        private readonly IExperienceRepository _repository;

        public GetAllQueryHandler(IExperienceRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<ExperiencesResponse>> Handle(GetAllExperiencesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Experience, ExperiencesResponse>> expression = e => new ExperiencesResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Code = e.Code,
            };
            var paginatedList = await _repository.Experiences
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}