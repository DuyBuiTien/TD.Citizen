using TD.Libs.ThrowR;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Extensions
{
    public static class QueryablePlaceTypeExtensions
    {

        public static IQueryable<PlaceType> FilterByCategoryId(this IQueryable<PlaceType> source, int? CategoryId)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (CategoryId < 0 || CategoryId.HasValue)
                return source;

            return source.Where(e => e.CategoryId == CategoryId);
        }



        public static IQueryable<PlaceType> Sort(this IQueryable<PlaceType> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Area>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.Name);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<PlaceType> Search(this IQueryable<PlaceType> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return source.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
}