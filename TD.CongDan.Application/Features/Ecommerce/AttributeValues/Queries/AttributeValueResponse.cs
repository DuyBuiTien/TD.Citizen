namespace TD.CongDan.Application.Features.AttributeValues.Queries
{
    public class AttributeValueResponse
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int? AttributeId { get; set; }
        public int Position { get; set; }
        public bool IsDefault { get; set; }
        public int Status { get; set; }

    }
}