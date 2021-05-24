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
    public static class QueryableJobAppliedExtensions
    {


        public static IQueryable<JobApplied> Sort(this IQueryable<JobApplied> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Recruitment.CreatedOn);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<JobApplied>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.CreatedOn);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<JobApplied> FilterUserName(this IQueryable<JobApplied> source, string userName)
        {
            return source.Where(e => e.UserName == userName);
        }

        public static IQueryable<JobApplied> FilterCurrentCompany(this IQueryable<JobApplied> source, int CompanyId)
        {
            return source.Where(e => e.Recruitment.CompanyId == CompanyId);
        }

        public static IQueryable<JobApplied> FilterRecruitmentId(this IQueryable<JobApplied> source, int RecruitmentId)
        {
            return source.Where(e => e.RecruitmentId == RecruitmentId);
        }

        public static IQueryable<JobApplied> Search(this IQueryable<JobApplied> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return source.Where(e => e.Recruitment.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
}