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
using TD.CongDan.Application.Features.Recruitments.Queries;

namespace TD.CongDan.Application.Features.JobSaveds.Queries
{
    public class GetAllJobSavedsQuery : IRequest<PaginatedResult<JobSavedsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllJobSavedsQuery(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllJobSavedsQuery, PaginatedResult<JobSavedsResponse>>
    {
        private readonly IJobSavedRepository _repository;
        private readonly IAuthenticatedUserService _authenticatedUser;


        public GetAllQueryHandler(IJobSavedRepository repository, IAuthenticatedUserService _authenticatedUser)
        {
            _repository = repository;
            this._authenticatedUser = _authenticatedUser;
        }

        public async Task<PaginatedResult<JobSavedsResponse>> Handle(GetAllJobSavedsQuery request, CancellationToken cancellationToken)
        {
            var userName = _authenticatedUser.Username;

            /* Expression<Func<JobSaved, JobSavedsResponse>> expression = e => new JobSavedsResponse
             {
                 UserName = e.UserName,
                 RecruitmentId = e.RecruitmentId,
                 Recruitment = e.Recruitment,
             };
             var paginatedList = await _repository.JobSaveds
                 .FilterUserName(userName)
                 .Search(request.KeySearch)
                 .Sort(request.OrderBy)
                 .Select(expression)
                 .ToPaginatedListAsync(request.PageNumber, request.PageSize);
             return paginatedList;*/

            Expression<Func<JobSaved, JobSavedsResponse>> expression = e => new JobSavedsResponse
            {
                Id = e.Recruitment.Id,
                Name = e.Recruitment.Name,
                Image = e.Recruitment.Image,
                CompanyId = e.Recruitment.CompanyId,
                CompanyName = e.Recruitment.Company.Name,
                CompanyLogo = e.Recruitment.Company.Logo,
                ResumeApplyExpired = e.Recruitment.ResumeApplyExpired,
                PlaceName = e.Recruitment.Name,
                PlaceProvince = e.Recruitment.Place.Province.NameWithType,
                PlaceDistrict = e.Recruitment.Place.District.NameWithType,
                PlaceCommune = e.Recruitment.Place.Commune.NameWithType,
                JobName = e.Recruitment.JobName.Name,
                JobPosition = e.Recruitment.JobPosition.Name,
                JobAge = e.Recruitment.JobAge.Name,
                Salary = e.Recruitment.Salary.Name
            };
            var paginatedList = await _repository.JobSaveds
                .FilterUserName(userName)
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;

        }
    }
}