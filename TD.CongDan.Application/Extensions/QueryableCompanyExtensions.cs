using TD.Libs.ThrowR;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Extensions
{
    public static class QueryableCompanyExtensions
    {

        public static IQueryable<Company> FilterCompanyByUserId(this IQueryable<Company> source, string UserId)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(UserId))
                return source;

            return source.Where(e => e.UserId == UserId);
        }

        public static IQueryable<Company> FilterCompanyByProvinceId(this IQueryable<Company> source, int? provinceId)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (provinceId == null || provinceId < 1)
                return source;
            return source.Where(s => s.Place.ProvinceId == provinceId);

        }
            public static IQueryable<Company> FilterCompanyByDistrictId(this IQueryable<Company> source, int? districtId)
            {
                Throw.Exception.IfNull(source, nameof(source));
                if (districtId == null || districtId < 1)
                    return source;
                return source.Where(s => s.Place.DistrictId == districtId);
            }

            public static IQueryable<Company> FilterCompanyByCommuneId(this IQueryable<Company> source, int? communeId)
            {
                Throw.Exception.IfNull(source, nameof(source));
                if (communeId == null || communeId < 1)
                    return source;
                return source.Where(s => s.Place.CommuneId == communeId);
            }

            /* public static IQueryable<Company> FilterAreaType(this IQueryable<Area> source, string Type)
             {
                 Throw.Exception.IfNull(source, nameof(source));
                 if (string.IsNullOrWhiteSpace(Type))
                     return source;

                 return source.Where(e => e.Type == Type);
             }*/

            /* public static IQueryable<Company> FilterAreaLevel(this IQueryable<Area> source, int? Level)
             {
                 Throw.Exception.IfNull(source, nameof(source));
                 if (Level==null)
                     return source;

                 return source.Where(e => e.Level == Level);
             }*/

            public static IQueryable<Company> Sort(this IQueryable<Company> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Area>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.Id);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<Company> Search(this IQueryable<Company> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();



            return source.Where(e => e.Name.ToLower().Contains(lowerCaseTerm) || e.InternationalName.ToLower().Contains(lowerCaseTerm) || e.ShortName.ToLower().Contains(lowerCaseTerm) || e.TaxCode.ToLower().Contains(lowerCaseTerm));
        }


        

    }
}