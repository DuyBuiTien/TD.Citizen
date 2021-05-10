using TD.Libs.ThrowR;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Extensions
{
    public static class QueryableUserExtensions
    {

        public static IQueryable<ApplicationUser> Sort(this IQueryable<ApplicationUser> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<ApplicationUser>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.Id);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<ApplicationUser> Search(this IQueryable<ApplicationUser> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();



            return source.Where(e => e.FirstName.ToLower().Contains(lowerCaseTerm));
        }
    }
    }