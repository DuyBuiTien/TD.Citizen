using System.Collections.Generic;
using System.Text.Json.Serialization;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Ecommerce
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string Image { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Images { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string VideoURL { get; set; }
        public decimal Rate { get; set; }

        //GIa ban thuc te Giá bán phải lớn hơn 0 và phải nhỏ hơn hoặc bằng Giá niêm yết và không được nhỏ hơn 10% giá trị của Giá niêm yết
        public decimal Price { get; set; }
        //Gia niem yet
        public decimal ListPrice { get; set; }
        //So luong
        public int Quantity { get; set; }

        //
        public int? BrandId { get; set; }
        //Danh muc san pham
        public int? PrimaryEcommerceCategoryId { get; set; }

        public int? CompanyId {get;set;}
        public string UserName { get; set; }
        
        public virtual Company.Company Company { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual EcommerceCategory PrimaryEcommerceCategory { get; set; }
        [JsonIgnore]
        public virtual ICollection<AttributeDatetime> AttributeDatetimes { get; set; }

        [JsonIgnore]
        public virtual ICollection<AttributeDecimal> AttributeDecimals { get; set; }

        [JsonIgnore]
        public virtual ICollection<AttributeInt> AttributeInts { get; set; }

        [JsonIgnore]
        public virtual ICollection<AttributeText> AttributeTexts { get; set; }
        [JsonIgnore]
        public virtual ICollection<AttributeVarchar> AttributeVarchars { get; set; }


        [JsonIgnore]
        public virtual ICollection<EcommerceCategoryProduct> EcommerceCategoryProducts { get; set; }

    }
}