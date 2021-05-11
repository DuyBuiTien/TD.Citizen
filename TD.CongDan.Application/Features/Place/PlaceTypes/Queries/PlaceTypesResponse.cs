using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.PlaceTypes.Queries
{
    public class PlaceTypesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public string  Category { get; set; }


    }
}