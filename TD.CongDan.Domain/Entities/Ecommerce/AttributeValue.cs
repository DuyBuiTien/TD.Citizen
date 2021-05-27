using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Ecommerce
{
    public class AttributeValue : AuditableEntity
    {
        public string Value { get; set; }
        public int? AttributeId { get; set; }
        public int Position { get; set; }
        public bool IsDefault { get; set; }
        public int Status { get; set; }
        public Attribute Attribute { get; set; }
    }
}
