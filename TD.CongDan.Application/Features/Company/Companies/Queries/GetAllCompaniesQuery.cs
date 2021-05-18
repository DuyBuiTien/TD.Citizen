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

namespace TD.CongDan.Application.Features.Companies.Queries
{
    public class GetAllCompaniesQuery : IRequest<PaginatedResult<CompaniesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }
        public string UserName { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? CommuneId { get; set; }

        public GetAllCompaniesQuery(int pageNumber, int pageSize, string keySearch, string orderBy, string userName, int? provinceId, int? districtId, int? communeId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
            UserName = userName;
            ProvinceId = provinceId;
            DistrictId = districtId;
            CommuneId = communeId;
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllCompaniesQuery, PaginatedResult<CompaniesResponse>>
    {
        private readonly ICompanyRepository _repository;

        public GetAllQueryHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<CompaniesResponse>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Company, CompaniesResponse>> expression = e => new CompaniesResponse
            {
                Id = e.Id,
                Name = e.Name,
                InternationalName = e.InternationalName,
                ShortName = e.ShortName,
                TaxCode = e.TaxCode,
                PlaceName = e.Place.PlaceName,
                Province = e.Place.Province.NameWithType,
                District = e.Place.District.NameWithType,
                Commune = e.Place.Commune.NameWithType,
                Description = e.Description,
                Image = e.Image,
                Logo = e.Logo,
                PhoneNumber = e.PhoneNumber,
                CompanySize = e.CompanySize,
                Email = e.Email,
            };
            var paginatedList = await _repository.Companies
                .FilterCompanyByUserName(request.UserName)
                .FilterCompanyByProvinceId(request.ProvinceId)
                .FilterCompanyByDistrictId(request.DistrictId)
                .FilterCompanyByCommuneId(request.CommuneId)
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}