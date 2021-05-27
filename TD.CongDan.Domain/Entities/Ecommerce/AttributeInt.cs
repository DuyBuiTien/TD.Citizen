using System;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Ecommerce
{
    public class AttributeInt : AuditableEntity
    {
        public int Value;
        public int? AttributeId;
        public int? ProductId;

        public Product Product;
        public Attribute Attribute;
    }
}
