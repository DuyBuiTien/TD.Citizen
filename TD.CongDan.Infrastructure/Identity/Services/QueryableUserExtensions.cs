using TD.Libs.ThrowR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace TD.CongDan.Application.Extensions
{
    public static class QueryableRoleExtensions
    {

        public static IQueryable<IdentityRole> Sort(this IQueryable<IdentityRole> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<IdentityRole>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.Id);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<IdentityRole> Search(this IQueryable<IdentityRole> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();



            return source.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
    }