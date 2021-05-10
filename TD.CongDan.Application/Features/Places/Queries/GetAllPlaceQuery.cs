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
using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Application.Features.Places.Queries
{
    public class GetAllPlaceQuery : IRequest<PaginatedResult<PlaceResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string ParentCode { get; set; }
        public string Type { get; set; }
        public string KeySearch { get; set; }
        public string OrderBy { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Range { get; set; }
        public string PlaceTypeId { get; set; }

        public GetAllPlaceQuery(int pageNumber, int pageSize, string parentCode, string type, string keySearch, string orderBy, double latitude, double longitude, double range, string placeTypeId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            ParentCode = parentCode;
            Type = type;
            KeySearch = keySearch;
            OrderBy = orderBy;
            Latitude = latitude;
            Longitude = longitude;
            Range = range;
            PlaceTypeId = placeTypeId;
        }
    }

    public class GGetAllQueryHandler : IRequestHandler<GetAllPlaceQuery, PaginatedResult<PlaceResponse>>
    {
        private readonly IPlaceRepository _repository;

        public GGetAllQueryHandler(IPlaceRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<PlaceResponse>> Handle(GetAllPlaceQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Place, PlaceResponse>> expression = e => new PlaceResponse
            {
                Id = e.Id,
                PlaceName = e.PlaceName,
                Title = e.Title,
                AddressDetail = e.AddressDetail,
                Source = e.Source,
                ExtraInfo = e.ExtraInfo,
                PhoneContact = e.PhoneContact,
                Website = e.Website,
                Email = e.Email,
                Content = e.Content,
                ContentHtml = e.ContentHtml,
                Latitude = e.Latitude,
                Longitude = e.Longitude,
                Tags = e.Tags,
                Image = e.Image,
                Images = e.Images,
                Status = e.Status,
                DateStart = e.DateStart,
                DateEnd = e.DateEnd,
                TimeStart = e.TimeStart,
                TimeEnd = e.TimeEnd,
                PlaceTypeId = e.PlaceTypeId,
                PlaceType = e.PlaceType.Name
            };
           var paginatedList = await _repository.Places
                .FilterPlaceByPlaceType(request.PlaceTypeId)
                .DistanceBetween2Points(request.Latitude, request.Longitude, request.Range)
                .Search(request.KeySearch)
                .Sort(request.OrderBy)
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }


    }
}
