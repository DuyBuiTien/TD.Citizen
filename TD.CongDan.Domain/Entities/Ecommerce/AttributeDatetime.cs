using System;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Ecommerce
{
    public class AttributeDatetime : AuditableEntity
    {
        public DateTime Value;
        public int? AttributeId;
        public int? ProductId;

        public Product Product;
        public Attribute Attribute;
    }
}
