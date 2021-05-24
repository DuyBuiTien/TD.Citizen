
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Ecommerce
{
    public class EcommerceCategoryProduct : AuditableEntity
    {
        public int? EcommerceCategoryId { get; set; }
        public int? ProductId { get; set; }
        public bool IsPrimary { get; set; }
        public Product Product { get; set; }
        public EcommerceCategory EcommerceCategory { get; set; }
        
    }
}
