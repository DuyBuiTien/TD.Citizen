using TD.Libs.ThrowR;
using System.Linq;
using System.Linq.Dynamic.Core;

using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Application.Extensions
{
    public static class QueryableAttributeExtensions
    {

       

        public static IQueryable<Attribute> Sort(this IQueryable<Attribute> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Attribute>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.Id);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<Attribute> Search(this IQueryable<Attribute> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return source.Where(e => e.Code.ToLower().Contains(lowerCaseTerm) || e.DisplayName.ToLower().Contains(lowerCaseTerm));
        }


    }
}