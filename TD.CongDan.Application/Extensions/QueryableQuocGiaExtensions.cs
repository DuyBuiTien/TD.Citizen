using TD.Libs.ThrowR;
using System.Linq;
using System.Linq.Dynamic.Core;
using TD.CongDan.Domain.Entities.Covid;
namespace TD.CongDan.Application.Extensions
{
    public static class QueryableQuocGiaExtensions
    {


        public static IQueryable<QuocGia> Sort(this IQueryable<QuocGia> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<QuocGia>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.Id);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<QuocGia> Search(this IQueryable<QuocGia> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return source.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
    }
}