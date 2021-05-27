using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Ecommerce
{
    public class ProductSaved : AuditableEntity
    {
        public string UserName { get; set; }
        public int? ProductId { get; set; }
        
        public Product Product { get; set; }

    }
}
