using System.Collections.Generic;
using System.Text.Json.Serialization;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Ecommerce
{
    public class EcommerceCategory : AuditableEntity
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public int Position { get; set; }
        public bool IncludeInMenu { get; set; }
        public int Level { get; set; }
        public string Icon { get; set; }
        public string Image { get;set; }
        public string[] Tags { get; set; }
        public int Status { get; set; }


        [JsonIgnore]
        public virtual ICollection<Product> PrimaryProducts { get; set; }
        [JsonIgnore]
        public virtual ICollection<EcommerceCategoryAttribute> EcommerceCategoryAttributes { get; set; }
        [JsonIgnore]
        public virtual ICollection<EcommerceCategoryProduct> EcommerceCategoryProducts { get; set; }

        

    }
}
