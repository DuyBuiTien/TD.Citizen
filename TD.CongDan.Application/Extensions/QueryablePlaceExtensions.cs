using TD.Libs.ThrowR;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Extensions
{
    public static class QueryablePlaceExtensions
    {

        public static IQueryable<Place> FilterPlaceByPlaceType(this IQueryable<Place> source, string PlaceTypeId)
        {

            Throw.Exception.IfNull(source, nameof(source));

            if (string.IsNullOrWhiteSpace(PlaceTypeId))
                return source;

            var orderParams = PlaceTypeId.Trim().Split(',');
            var orderQueryBuilder = new StringBuilder();
            return source.Where(e => orderParams.Contains(e.PlaceTypeId.ToString()));
        }

        public static IQueryable<Place> DistanceBetween2Points(this IQueryable<Place> source, double latitude, double longitude, double range)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (latitude.Equals(null) || latitude == 0 || longitude.Equals(null) || longitude == 0 || range.Equals(null) || range == 0)
                return source;
            return source.Where(x => Math.Acos(Math.Sin(Math.PI * x.Latitude / 180) * Math.Sin(Math.PI * latitude / 180) + Math.Cos(Math.PI * x.Latitude / 180) * Math.Cos(Math.PI * latitude / 180) * Math.Cos(Math.PI / 180 * (x.Longitude - longitude))) * 180 / Math.PI * 60 * 1.1515 * 1.609344 < range);
        }

        public static IQueryable<Place> Sort(this IQueryable<Place> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.PlaceName);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Place>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.PlaceName);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<Place> Search(this IQueryable<Place> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return source.Where(e => e.PlaceName.ToLower().Contains(lowerCaseTerm));
        }

    }
}