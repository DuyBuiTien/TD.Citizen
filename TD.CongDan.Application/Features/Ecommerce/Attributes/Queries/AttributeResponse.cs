using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Application.Features.Attributes.Queries
{
    public class AttributeResponse
    {
        public int Id { get; set; }
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

    }
}