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
using TD.CongDan.Application.Interfaces.Shared;

namespace TD.CongDan.Application.Features.JobApplieds.Queries
{
    public class GetAllJobAppliedsQuery : IRequest<PaginatedResult<JobAppliedsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllJobAppliedsQuery(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllJobAppliedsQuery, PaginatedResult<JobAppliedsResponse>>
    {
        private readonly IJobAppliedRepository _repository;
        private readonly IAuthenticatedUserService _authenticatedUser;


        public GetAllQueryHandler(IJobAppliedRepository repository, IAuthenticatedUserService _authenticatedUser)
        {
            _repository = repository;
            this._authenticatedUser = _authenticatedUser;
        }

        public async Task<PaginatedResult<JobAppliedsResponse>> Handle(GetAllJobAppliedsQuery request, CancellationToken cancellationToken)
        {
            var userName = _authenticatedUser.Username;

            Expression<Func<JobApplied, JobAppliedsResponse>> expression = e => new JobAppliedsResponse
            {
                UserName = e.UserName,
                RecruitmentId = e.RecruitmentId,
                Recruitment = e.Recruitment,
                CVFile = e.CVFile
            };
            var paginatedList = await _repository.JobApplieds
                .FilterUserName(userName)
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}