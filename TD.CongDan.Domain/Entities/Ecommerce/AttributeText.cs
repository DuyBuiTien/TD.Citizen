using System;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Ecommerce
{
    public class AttributeText : AuditableEntity
    {
        public string Value;
        public int? AttributeId;
        public int? ProductId;

        public Product Product;
        public Attribute Attribute;
    }
}
