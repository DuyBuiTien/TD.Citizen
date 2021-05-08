using System.Collections.Generic;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Infrastructure.Entities
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }

        public ICollection<PlaceType> PlaceTypes { get; set; }

    }
}
