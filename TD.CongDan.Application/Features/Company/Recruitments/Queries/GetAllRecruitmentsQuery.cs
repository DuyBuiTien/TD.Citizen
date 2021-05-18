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

namespace TD.CongDan.Application.Features.Recruitments.Queries
{
    public class GetAllRecruitmentsQuery : IRequest<PaginatedResult<RecruitmentsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }
        public int? CompanyId { get; set; }
        public string UserName {get;set;}
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? CommuneId { get; set; }
        public int? JobTypeId { get; set; }
        public int? JobNameId { get; set; }
        public int? JobPositionId { get; set; }
        public int? SalaryId { get; set; }
        public int? ExperienceId { get; set; }
        public int? GenderId { get; set; }
        public int? JobAgeId { get; set; }
        //Bang cap
        public int? DegreeId { get; set; }


        public GetAllRecruitmentsQuery(int pageNumber, int pageSize, string keySearch, string orderBy, int? companyId, string userName, int? provinceId, int? districtId, int? communeId, int? jobTypeId, int? jobNameId,
            int? jobPositionId, int? salaryId, int? experienceId, int? genderId, int? jobAgeId, int degreeId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
            CompanyId = companyId;
            UserName = userName;
            ProvinceId = provinceId;
            DistrictId = districtId;
            CommuneId = communeId;
            JobTypeId = jobTypeId;
            JobNameId = jobNameId;
            JobPositionId = jobPositionId;
            JobAgeId = jobAgeId;
            SalaryId = salaryId;
            ExperienceId = experienceId;
            GenderId = genderId;
            DegreeId = degreeId;

        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllRecruitmentsQuery, PaginatedResult<RecruitmentsResponse>>
    {
        private readonly IRecruitmentRepository _repository;

        public GetAllQueryHandler(IRecruitmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<RecruitmentsResponse>> Handle(GetAllRecruitmentsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Recruitment, RecruitmentsResponse>> expression = e => new RecruitmentsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Image = e.Image,
                CompanyId = e.CompanyId,
                CompanyName = e.Company.Name,
                CompanyLogo = e.Company.Logo,
                ResumeApplyExpired = e.ResumeApplyExpired,
                PlaceName = e.Name,
                PlaceProvince = e.Place.Province.NameWithType,
                PlaceDistrict = e.Place.District.NameWithType,
                PlaceCommune = e.Place.Commune.NameWithType,
                JobName = e.JobName.Name,
                JobPosition = e.JobPosition.Name,
                JobAge = e.JobAge.Name,
                Salary = e.Salary.Name
            };
            var paginatedList = await _repository.Recruitments
                .FilterRecruitmentByUserName(request.UserName)
                .FilterRecruitmentByCompanyId(request.CompanyId)
                .FilterRecruitmentByProvinceId(request.ProvinceId)
                .FilterRecruitmentByDistrictId(request.DistrictId)
                .FilterRecruitmentByCommuneId(request.CommuneId)
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}