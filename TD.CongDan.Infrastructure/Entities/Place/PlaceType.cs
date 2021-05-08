using TD.Libs.Abstractions.Domain;
using System.Collections.Generic;


namespace TD.CongDan.Infrastructure.Entities
{
    public class PlaceType : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<Place> Places { get; set; }
    }
 
}
