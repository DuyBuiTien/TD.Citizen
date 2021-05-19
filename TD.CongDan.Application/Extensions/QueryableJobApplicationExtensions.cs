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
    public static class QueryableJobApplicationExtensions
    {


        public static IQueryable<JobApplication> FillterByUsername(this IQueryable<JobApplication> source, string UserName)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(UserName))
                return source;
            return source.Where(e => e.UserName == UserName);
        }

        public static IQueryable<JobApplication> FillterByCurrentPositionId(this IQueryable<JobApplication> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id<0 || id ==null)
                return source;
            return source.Where(e => e.CurrentPositionId == id);
        }

        public static IQueryable<JobApplication> FillterByPositionId(this IQueryable<JobApplication> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id < 0 || id == null)
                return source;
            return source.Where(e => e.PositionId == id);
        }
        public static IQueryable<JobApplication> FillterByDegreeId(this IQueryable<JobApplication> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id < 0 || id == null)
                return source;
            return source.Where(e => e.DegreeId == id);
        }

        public static IQueryable<JobApplication> FillterByExperienceId(this IQueryable<JobApplication> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id < 0 || id == null)
                return source;
            return source.Where(e => e.ExperienceId == id);
        }

        public static IQueryable<JobApplication> FillterByJobTypeId(this IQueryable<JobApplication> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id < 0 || id == null)
                return source;
            return source.Where(e => e.JobTypeId == id);
        }
        public static IQueryable<JobApplication> FillterByIsSearchAllowed(this IQueryable<JobApplication> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id < 0 || id == null)
                return source;
            return source.Where(e => e.IsSearchAllowed == id);
        }


        public static IQueryable<JobApplication> Sort(this IQueryable<JobApplication> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<JobApplication>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.Id);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<JobApplication> Search(this IQueryable<JobApplication> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return source.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
}