using TD.Libs.ThrowR;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Application.Extensions
{
    public static class QueryableEcommerceCategoryExtensions
    {

        public static IQueryable<EcommerceCategory> FilterParentId(this IQueryable<EcommerceCategory> source, int? ParentId)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (ParentId==null || ParentId<1)
                return source;

            return source.Where(e => e.ParentId == ParentId);
        }

       

        public static IQueryable<EcommerceCategory> FilterLevel(this IQueryable<EcommerceCategory> source, int? Level)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (Level==null || Level<1)
                return source;
          

            return source.Where(e => e.Level == Level);
        }

        public static IQueryable<EcommerceCategory> Sort(this IQueryable<EcommerceCategory> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<EcommerceCategory>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.Id);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<EcommerceCategory> Search(this IQueryable<EcommerceCategory> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return source.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }


    }
}