using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Ecommerce
{
    public class ProductReview : AuditableEntity
    {
        public string UserName { get; set; }
        public int? ProductId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public string Images { get; set; }
        public Product Product { get; set; }
        public int Like { get; set; }

    }
}
