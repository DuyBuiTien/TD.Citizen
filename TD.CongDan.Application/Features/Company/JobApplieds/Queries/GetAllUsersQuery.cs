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

namespace TD.CongDan.Application.Features.JobApplieds.Queries
{
    public class GetAllUsersQuery : IRequest<PaginatedResult<JobApplicationsResponse>>
    {
        public int RecruitmentId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public GetAllUsersQuery(int RecruitmentId, int pageNumber, int pageSize, string keySearch, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
            this.RecruitmentId = RecruitmentId;
        }
    }

    public class GetAllUserQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedResult<JobApplicationsResponse>>
    {
        private readonly IJobAppliedRepository _repository;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAllUserQueryHandler(IJobAppliedRepository repository, IAuthenticatedUserService _authenticatedUser, IJobApplicationRepository _jobApplicationRepository, UserManager<ApplicationUser> _userManager)
        {
            _repository = repository;
            this._authenticatedUser = _authenticatedUser;
            this._jobApplicationRepository = _jobApplicationRepository;
            this._userManager = _userManager;
        }

        public async Task<PaginatedResult<JobApplicationsResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var userName = _authenticatedUser.Username;

            Expression<Func<JobApplied, JobApplicationsResponse>> expression = e => new JobApplicationsResponse
            {
                UserName = e.UserName,
               
            };
            var paginatedList = await _repository.JobApplieds
                .FilterRecruitmentId(request.RecruitmentId)
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