using System.Collections.Generic;
using System.Text.Json.Serialization;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Ecommerce
{
    public class Brand : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}