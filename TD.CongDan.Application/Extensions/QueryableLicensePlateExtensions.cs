using TD.Libs.ThrowR;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Extensions
{
    public static class QueryableLicensePlateExtensions
    {

        public static IQueryable<LicensePlate> FilterByUsername(this IQueryable<LicensePlate> source, string UserName)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(UserName))
                return source;

            return source.Where(e => e.UserName == UserName);
        }



        public static IQueryable<LicensePlate> Sort(this IQueryable<LicensePlate> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<LicensePlate>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.Name);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<LicensePlate> Search(this IQueryable<LicensePlate> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return source.Where(e => e.LicensePlateNumber.ToLower().Contains(lowerCaseTerm) || e.OwnerFullName.ToLower().Contains(lowerCaseTerm) || e.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
}