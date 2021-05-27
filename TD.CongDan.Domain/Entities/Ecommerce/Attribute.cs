using System.Collections.Generic;
using System.Text.Json.Serialization;
using TD.CongDan.Domain.Enums;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Ecommerce
{
    public class Attribute : AuditableEntity
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool IsVisibleOnFront { get; set; }
        public bool IsRequired { get; set; }
        public bool IsFilterable { get; set; }
        public bool IsSearchable { get; set; }
        public bool IsEditable { get; set; }
        public bool IsSellerEditable { get; set; }
        public string DefaultValue { get; set; }
        public FrontendInput FrontendInput { get; set; }
        //Datatype : int, decimal, varchar, text, datetime
        public DataType DataType { get; set; }
        public FrontendInput InputType { get; set; }

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
        public virtual ICollection<AttributeValue> AttributeValues { get; set; }

        [JsonIgnore]
        public virtual ICollection<EcommerceCategoryAttribute> EcommerceCategoryAttributes { get; set; }

    }
}
