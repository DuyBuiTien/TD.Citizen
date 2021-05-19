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
using Microsoft.AspNetCore.Identity;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.JobApplications.Queries
{
    public class GetAllJobApplicationsQuery : IRequest<PaginatedResult<JobApplicationsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }

        public int? CurrentPositionId { get; set; }
        //Vi tri mong muon
        public int? PositionId { get; set; }
        //Trinh do hoc van
        public int? DegreeId { get; set; }
        //Tong so nam Kinh nghiem
        public int? ExperienceId { get; set; }
        public string UserName { get; set; }
        public int? JobTypeId { get; set; }
        //Cho phep nguoi khac tim kiem thong tin
        public int? IsSearchAllowed { get; set; }

        public GetAllJobApplicationsQuery(int pageNumber, int pageSize, string keySearch, string orderBy, string userName, int?currentPositionId, int? positionId, int? degreeId, int? experienceId, int? jobTypeId, int? isSearchAllowed)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
            UserName = userName;
            CurrentPositionId = currentPositionId;
            PositionId = positionId;
            DegreeId = degreeId;
            ExperienceId = experienceId;
            JobTypeId = jobTypeId;
            IsSearchAllowed = isSearchAllowed;
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllJobApplicationsQuery, PaginatedResult<JobApplicationsResponse>>
    {
        private readonly IJobApplicationRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAllQueryHandler(IJobApplicationRepository repository, UserManager<ApplicationUser> _userManager)
        {
            _repository = repository;
            this._userManager = _userManager;
        }

        public async Task<PaginatedResult<JobApplicationsResponse>> Handle(GetAllJobApplicationsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<JobApplication, JobApplicationsResponse>> expression = e => new JobApplicationsResponse
            {
                Id = e.Id,
                Name = e.Name,
                UserName = e.UserName,
                CurrentPosition = e.CurrentPosition,
                CurrentPositionId = e.CurrentPositionId,
                Degree = e.Degree,
                DegreeId = e.DegreeId,
                ExperienceId = e.ExperienceId,
                Experience = e.Experience,
                JobType = e.JobType,
                JobTypeId = e.JobTypeId,
            };
            var paginatedList = await _repository.JobApplications
                .FillterByUsername(request.UserName)
                .FillterByPositionId(request.PositionId)
                .FillterByJobTypeId(request.JobTypeId)
                .FillterByIsSearchAllowed(request.IsSearchAllowed)
                .FillterByExperienceId(request.ExperienceId)
                .FillterByDegreeId(request.DegreeId)
                .FillterByCurrentPositionId(request.CurrentPositionId)
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            foreach (var data in paginatedList.Data)
            {
                var tmp = data.UserName;

                var user = await _userManager.FindByNameAsync(data.UserName);

                if (user !=null)
                {
                    data.UserFirstName = user.FirstName;
                    data.UserLastName = user.LastName;
                    data.UserAvatarUrl = user.AvatarUrl;
                    
                }
                
            }
            


            return paginatedList;
        }
    }
}