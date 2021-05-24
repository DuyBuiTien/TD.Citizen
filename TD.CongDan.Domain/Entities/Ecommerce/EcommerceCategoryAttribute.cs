
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Ecommerce
{
    public class EcommerceCategoryAttribute : AuditableEntity
    {
        public int? EcommerceCategoryId { get; set; }
        public int? AttributeId { get; set; }
        public Attribute Attribute { get; set; }
        public EcommerceCategory EcommerceCategory { get; set; }
        
    }
}
