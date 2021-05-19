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
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Features.Carpools.Queries
{
    public class GetAllCarpoolsQuery : IRequest<PaginatedResult<CarpoolsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceTo { get; set; }

        public int? DepartureProvinceId { get; set; }
        public int? DepartureDistrictId { get; set; }
        public int? DepartureCommuneId { get; set; }

        public int? ArrivalProvinceId { get; set; }
        public int? ArrivalDistrictId { get; set; }
        public int? ArrivalCommuneId { get; set; }

        public string DepartureDateStart { get; set; }
        public string DepartureDateEnd { get; set; }
        public int? Status { get; set; }
        public string UserName { get; set; }


        public GetAllCarpoolsQuery(int pageNumber, int pageSize, string keySearch, string userName, string orderBy, decimal? price, decimal? priceTo, int? departureProvinceId, int? departureDistrictId, int? departureCommuneId, int? arrivalProvinceId, int? arrivalDistrictId, int? arrivalCommuneId, string departureDateStart, string departureDateEnd, int? status)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            KeySearch = keySearch;
            OrderBy = orderBy;
            Price = price;
            DepartureProvinceId = departureProvinceId;
            DepartureDistrictId = departureDistrictId;
            DepartureCommuneId = departureCommuneId;
            ArrivalProvinceId = arrivalProvinceId;
            ArrivalDistrictId = arrivalDistrictId;
            ArrivalCommuneId = arrivalCommuneId;
            DepartureDateStart = departureDateStart;
            DepartureDateEnd = departureDateEnd;
            Status = status;
            UserName = userName;
            PriceTo = priceTo;
        }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllCarpoolsQuery, PaginatedResult<CarpoolsResponse>>
    {
        private readonly ICarpoolRepository _repository;

        public GetAllQueryHandler(ICarpoolRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<CarpoolsResponse>> Handle(GetAllCarpoolsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Carpool, CarpoolsResponse>> expression = e => new CarpoolsResponse
            {
                Id = e.Id,
                Name = e.Name,
                PhoneNumber = e.PhoneNumber,
                Description = e.Description,
                SeatCount = e.SeatCount,
                Status = e.Status,
                UserName = e.UserName,
                PlaceDepartureId = e.PlaceDepartureId,
                PlaceDeparture = e.PlaceDeparture,
                PlaceArrivalId = e.PlaceArrivalId,
                PlaceArrival = e.PlaceArrival,
                DepartureDate = e.DepartureDate,
                DepartureTime = e.DepartureTime,
                VehicleTypeId = e.VehicleTypeId,
                VehicleType = e.VehicleType,
                Role = e.Role,
                Price = e.Price,


            };
            var paginatedList = await _repository.Carpools
                .FilterDepartureDateStart(request.DepartureDateStart)
                .FilterDepartureDateEnd(request.DepartureDateEnd)
                .FilterArrivalCommuneId(request.ArrivalCommuneId)
                .FilterArrivalDistrictId(request.ArrivalDistrictId)
                .FilterArrivalProvinceId(request.ArrivalProvinceId)
                .FilterCarpoolByUserName(request.UserName)
                .FilterDepartureCommuneId(request.DepartureCommuneId)
                .FilterDepartureDistrictId(request.DepartureDistrictId)
                .FilterDepartureProvinceId(request.DepartureProvinceId)
                .FilterPriceTo(request.PriceTo)
                .FilterPrice(request.Price)
                .FilterStatus(request.Status)
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}