using TD.Libs.ThrowR;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Extensions
{
    public static class QueryableAreaExtensions
    {

        public static IQueryable<Area> FilterAreaParentCode(this IQueryable<Area> source, string ParentCode)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(ParentCode))
                return source;

            return source.Where(e => e.ParentCode == ParentCode);
        }

        public static IQueryable<Area> FilterAreaType(this IQueryable<Area> source, string Type)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(Type))
                return source;

            return source.Where(e => e.Type == Type);
        }

        public static IQueryable<Area> Sort(this IQueryable<Area> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Area>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.Id);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<Area> Search(this IQueryable<Area> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();

            //return source.Where(delegate (Area c)
            //{
            //    if (ConvertToUnSign(c.NameWithType).IndexOf(lowerCaseTerm, StringComparison.CurrentCultureIgnoreCase) >= 0)
            //       return true;
            //    else
            //        return false;
            //}).AsQueryable();


            return source.Where(e => e.NameWithType.ToLower().Contains(lowerCaseTerm));
        }


        private static string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }

    }
}