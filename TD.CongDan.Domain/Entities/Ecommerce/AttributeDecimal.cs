using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Ecommerce
{
    public class AttributeDecimal : AuditableEntity
    {
        public decimal Value;
        public int? AttributeId;
        public int? ProductId;

        public Product Product;
        public Attribute Attribute;
    }
}
