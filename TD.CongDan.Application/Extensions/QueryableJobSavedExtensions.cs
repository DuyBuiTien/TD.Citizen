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
    public static class QueryableJobSavedExtensions
    {


        public static IQueryable<JobSaved> Sort(this IQueryable<JobSaved> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Recruitment.CreatedOn);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<JobSaved>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.CreatedOn);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<JobSaved> FilterUserName(this IQueryable<JobSaved> source, string userName)
        {
            return source.Where(e => e.UserName == userName);
        }

        public static IQueryable<JobSaved> Search(this IQueryable<JobSaved> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return source.Where(e => e.Recruitment.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
}