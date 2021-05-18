using TD.Libs.ThrowR;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;
using System.Globalization;

namespace TD.CongDan.Application.Extensions
{
    public static class QueryableRecruitmentExtensions
    {


        public static IQueryable<Recruitment> FilterRecruitmentByResumeApplyExpiredStartDate(this IQueryable<Recruitment> source, string date)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(date))
                return source;

            try
            {
                var sDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return source.Where(e => e.ResumeApplyExpired >= sDate);
            }
            catch {
                return source;
            }

        }

        public static IQueryable<Recruitment> FilterRecruitmentByResumeApplyExpiredEndDate(this IQueryable<Recruitment> source, string date)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(date))
                return source;

            try
            {
                var sDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return source.Where(e => e.ResumeApplyExpired <= sDate);
            }
            catch
            {
                return source;
            }

        }


        public static IQueryable<Recruitment> FilterRecruitmentByUserName(this IQueryable<Recruitment> source, string UserName)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(UserName))
                return source;

            return source.Where(e => e.UserName == UserName);
        }

        public static IQueryable<Recruitment> FilterRecruitmentByStatus(this IQueryable<Recruitment> source, int? status)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (status == null || status < 1)
                return source;

            return source.Where(e => e.Status == status);
        }

        public static IQueryable<Recruitment> FilterRecruitmentByCompanyId(this IQueryable<Recruitment> source, int? companyId)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (companyId==null || companyId<1)
                return source;

            return source.Where(e => e.CompanyId == companyId);
        }


        public static IQueryable<Recruitment> FilterRecruitmentByJobTypeId(this IQueryable<Recruitment> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id == null || id < 1)
                return source;
            return source.Where(s => s.JobTypeId == id);
        }

        public static IQueryable<Recruitment> FilterRecruitmentByJobNameId(this IQueryable<Recruitment> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id == null || id < 1)
                return source;
            return source.Where(s => s.JobNameId == id);
        }

        public static IQueryable<Recruitment> FilterRecruitmentByJobPositionId(this IQueryable<Recruitment> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id == null || id < 1)
                return source;
            return source.Where(s => s.JobPositionId == id);
        }

        public static IQueryable<Recruitment> FilterRecruitmentBySalaryId(this IQueryable<Recruitment> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id == null || id < 1)
                return source;
            return source.Where(s => s.SalaryId == id);
        }

        public static IQueryable<Recruitment> FilterRecruitmentByExperienceId(this IQueryable<Recruitment> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id == null || id < 1)
                return source;
            return source.Where(s => s.ExperienceId == id);
        }

        public static IQueryable<Recruitment> FilterRecruitmentByGenderId(this IQueryable<Recruitment> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id == null || id < 1)
                return source;
            return source.Where(s => s.GenderId == id);
        }


        public static IQueryable<Recruitment> FilterRecruitmentByJobAgeId(this IQueryable<Recruitment> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id == null || id < 1)
                return source;
            return source.Where(s => s.JobAgeId == id);
        }

        public static IQueryable<Recruitment> FilterRecruitmentByDegreeId(this IQueryable<Recruitment> source, int? id)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (id == null || id < 1)
                return source;
            return source.Where(s => s.DegreeId == id);
        }


        public static IQueryable<Recruitment> FilterRecruitmentByProvinceId(this IQueryable<Recruitment> source, int? provinceId)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (provinceId == null || provinceId < 1)
                return source;
            return source.Where(s => s.Place.ProvinceId == provinceId);

        }
            public static IQueryable<Recruitment> FilterRecruitmentByDistrictId(this IQueryable<Recruitment> source, int? districtId)
            {
                Throw.Exception.IfNull(source, nameof(source));
                if (districtId == null || districtId < 1)
                    return source;
                return source.Where(s => s.Place.DistrictId == districtId);
            }

            public static IQueryable<Recruitment> FilterRecruitmentByCommuneId(this IQueryable<Recruitment> source, int? communeId)
            {
                Throw.Exception.IfNull(source, nameof(source));
                if (communeId == null || communeId < 1)
                    return source;
                return source.Where(s => s.Place.CommuneId == communeId);
            }

           

            public static IQueryable<Recruitment> Sort(this IQueryable<Recruitment> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Area>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.Id);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<Recruitment> Search(this IQueryable<Recruitment> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();



            return source.Where(e => e.Name.ToLower().Contains(lowerCaseTerm) 
            || e.Company.Name.ToLower().Contains(lowerCaseTerm) 
            || e.JobName.Name.ToLower().Contains(lowerCaseTerm)
            || e.JobPosition.Name.ToLower().Contains(lowerCaseTerm)
            || e.JobType.Name.ToLower().Contains(lowerCaseTerm)
            || e.Degree.Name.ToLower().Contains(lowerCaseTerm)
            );
        }


        

    }
}