using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.Are.Queries
{
    public class AreaResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public Area ParentArea { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public string NameWithType { get; set; }
        public string Path { get; set; }
        public string PathWithType { get; set; }
        public string Description { get; set; }

    }
}