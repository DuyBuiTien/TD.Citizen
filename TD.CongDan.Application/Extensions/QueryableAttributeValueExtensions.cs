using TD.Libs.ThrowR;
using System.Linq;
using System.Linq.Dynamic.Core;

using TD.CongDan.Domain.Entities.Ecommerce;

namespace TD.CongDan.Application.Extensions
{
    public static class QueryableAttributeValueExtensions
    {

        public static IQueryable<AttributeValue> FilterByAttributeId(this IQueryable<AttributeValue> source, int? attributeId)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (attributeId==null || attributeId< 1)
                return source;

            return source.Where(e => e.AttributeId == attributeId);
        }

        public static IQueryable<AttributeValue> Sort(this IQueryable<AttributeValue> source, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return source.OrderBy(e => e.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<AttributeValue>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return source.OrderBy(e => e.Id);

            return source.OrderBy(orderQuery);
        }

        public static IQueryable<AttributeValue> Search(this IQueryable<AttributeValue> source, string searchTerm)
        {
            Throw.Exception.IfNull(source, nameof(source));
            if (string.IsNullOrWhiteSpace(searchTerm))
                return source;
            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return source.Where(e => e.Value.ToLower().Contains(lowerCaseTerm) );
        }


    }
}