using TD.Libs.Results;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Application.Extensions;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Features.LicensePlates.Queries
{
    public class GetAllLicensePlatesQuery : IRequest<PaginatedResult<LicensePlatesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }
        public string UserName { get; set; }

        public GetAllLicensePlatesQuery(int pageNumber, int pageSize, string keySearch, string orderBy, string userName)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
           
            KeySearch = keySearch;
            OrderBy = orderBy;
            UserName = userName;
        }
    }

    public class GGetAllQueryHandler : IRequestHandler<GetAllLicensePlatesQuery, PaginatedResult<LicensePlatesResponse>>
    {
        private readonly ILicensePlateRepository _repository;

        public GGetAllQueryHandler(ILicensePlateRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<LicensePlatesResponse>> Handle(GetAllLicensePlatesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<LicensePlate, LicensePlatesResponse>> expression = e => new LicensePlatesResponse
            {
                Id = e.Id,
                Name = e.Name,
                UserName  = e.UserName,
                OwnerFullName = e.OwnerFullName,
                LicensePlateNumber = e.LicensePlateNumber,
                DateOfRegistration = e.DateOfRegistration,
                Description = e.Description
            };


            var paginatedList = await _repository.LicensePlates
                .FilterByUsername(request.UserName)
                .Search(request.KeySearch)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }


    }
}
