using TD.Libs.Abstractions.Domain;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TD.CongDan.Domain.Entities.Other;

namespace TD.CongDan.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<PlaceType> PlaceTypes { get; set; }
        [JsonIgnore]
        public ICollection<Bookmark> Bookmarks { get; set; }

    }
}
