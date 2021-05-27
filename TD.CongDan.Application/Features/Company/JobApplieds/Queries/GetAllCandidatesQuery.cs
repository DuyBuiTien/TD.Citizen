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
using TD.CongDan.Application.Features.JobApplications.Queries;
using Microsoft.AspNetCore.Identity;
using TD.CongDan.Domain.Entities;
using TD.Libs.ThrowR;

namespace TD.CongDan.Application.Features.JobApplieds.Queries
{
    public class GetAllCandidatesQuery : IRequest<PaginatedResult<JobApplicationsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllCandidatesQuery(int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
        }
    }

    public class GetAllCandidatesQueryHandler : IRequestHandler<GetAllCandidatesQuery, PaginatedResult<JobApplicationsResponse>>
    {
        private readonly IJobAppliedRepository _repository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAllCandidatesQueryHandler(ICompanyRepository _companyRepository, IJobAppliedRepository repository, IAuthenticatedUserService _authenticatedUser, IJobApplicationRepository _jobApplicationRepository, UserManager<ApplicationUser> _userManager)
        {
            _repository = repository;
            this._authenticatedUser = _authenticatedUser;
            this._jobApplicationRepository = _jobApplicationRepository;
            this._userManager = _userManager;
            this._companyRepository = _companyRepository;
        }

        public async Task<PaginatedResult<JobApplicationsResponse>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
        {
            var userName = _authenticatedUser.Username;

            var company = await _companyRepository.GetByUserNameAsync(userName);
            Throw.Exception.IfNull(company, "Company", "No Company Found");


            Expression<Func<JobApplied, JobApplicationsResponse>> expression = e => new JobApplicationsResponse
            {
                UserName = e.UserName,
               
            };
            var paginatedList = await _repository.JobApplieds
                .FilterCurrentCompany(company.Id)
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);


            foreach (var data in paginatedList.Data)
            {
                var tmp = data.UserName;

                var user = await _userManager.FindByNameAsync(data.UserName);
                var job =await  _jobApplicationRepository.GetByUsernameAsync(data.UserName);

                if (user != null)
                {
                    data.UserFirstName = user.FirstName;
                    data.UserLastName = user.LastName;
                    data.UserAvatarUrl = user.AvatarUrl;
                }

                if (job!=null)
                {
                    data.CurrentPosition = job.CurrentPosition;
                    data.CurrentPositionId = job.CurrentPositionId;
                    data.Degree = job.Degree;
                    data.DegreeId = job.DegreeId;
                    data.ExperienceId = job.ExperienceId;
                    data.Experience = job.Experience;
                    data.JobType = job.JobType;
                    data.JobTypeId = job.JobTypeId;
                }

            }


            return paginatedList;
        }
    }
}