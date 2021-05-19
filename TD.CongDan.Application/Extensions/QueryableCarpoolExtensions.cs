using TD.Libs.ThrowR;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Traffic;
using System.Globalization;

namespace TD.CongDan.Application.Extensions
{
    public static class QueryableCarpoolExtensions
    {

        public static IQueryable<Carpool> FilterDepartureDateStart(this IQueryable<Carpool> source, string date)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(date))
                return source;

            try
            {
                var sDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return source.Where(e => e.DepartureDate >= sDate);
            }
            catch
            {
                return source;
            }

        }

        public static IQueryable<Carpool> FilterDepartureDateEnd(this IQueryable<Carpool> source, string date)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(date))
                return source;

            try
            {
                var sDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return source.Where(e => e.DepartureDate <= sDate);
            }
            catch
            {
                return source;
            }

        }



        public static IQueryable<Carpool> FilterCarpoolByUserName(this IQueryable<Carpool> source, string UserName)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(UserName))
                return source;

            return source.Where(e => e.UserName == UserName);
        }


        public static IQueryable<Carpool> FilterStatus(this IQueryable<Carpool> source, int? status)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (status == null || status < 0)
                return source;
            return source.Where(s => s.Status == status);

        }

        public static IQueryable<Carpool> FilterDepartureProvinceId(this IQueryable<Carpool> source, int? provinceId)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (provinceId == null || provinceId < 1)
                return source;
            return source.Where(s => s.PlaceDeparture.ProvinceId == provinceId);

        }
        public static IQueryable<Carpool> FilterDepartureDistrictId(this IQueryable<Carpool> source, int? districtId)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (districtId == null || districtId < 1)
                return source;
            return source.Where(s => s.PlaceDeparture.DistrictId == districtId);
        }

        public static IQueryable<Carpool> FilterDepartureCommuneId(this IQueryable<Carpool> source, int? communeId)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (communeId == null || communeId < 1)
                return source;
            return source.Where(s => s.PlaceDeparture.CommuneId == communeId);
        }


        public static IQueryable<Carpool> FilterArrivalProvinceId(this IQueryable<Carpool> source, int? provinceId)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (provinceId == null || provinceId < 1)
                return source;
            return source.Where(s => s.PlaceArrival.ProvinceId == provinceId);

        }
        public static IQueryable<Carpool> FilterArrivalDistrictId(this IQueryable<Carpool> source, int? districtId)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (districtId == null || districtId < 1)
                return source;
            return source.Where(s => s.PlaceArrival.DistrictId == districtId);
        }

        public static IQueryable<Carpool> FilterArrivalCommuneId(this IQueryable<Carpool> source, int? communeId)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (communeId == null || communeId < 1)
                return source;
            return source.Where(s => s.PlaceArrival.CommuneId == communeId);
        }



        public static IQueryable<Carpool> FilterPrice(this IQueryable<Carpool> source, decimal? price)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (price == null )
                return source;
            return source.Where(s => s.Price >= price);
        }

        public static IQueryable<Carpool> FilterPriceTo(this IQueryable<Carpool> source, decimal? price)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (price == null)
                return source;
            return source.Where(s => s.Price <= price);
        }


        public static IQueryable<Carpool> Sort(this IQueryable<Carpool> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Area>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.Id);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<Carpool> Search(this IQueryable<Carpool> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return source.Where(e => e.Name.ToLower().Contains(lowerCaseTerm) || e.PlaceDeparture.PlaceName.ToLower().Contains(lowerCaseTerm) || e.PlaceArrival.PlaceName.ToLower().Contains(lowerCaseTerm));
        }




    }
}