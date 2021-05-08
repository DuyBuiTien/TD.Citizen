using TD.Libs.Abstractions.Domain;
using System.Collections.Generic;

namespace TD.CongDan.Domain.Entities
{
    public class Area : AuditableEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }

        public string NameWithType { get; set; }
        public string Path { get; set; }
        public string PathWithType { get; set; }
        public string Description { get; set; }


        public ICollection<UserInfo> UserInfoProvinces { get; set; }
        public ICollection<UserInfo> UserInfoDistricts { get; set; }
        public ICollection<UserInfo> UserInfoCommunes { get; set; }

        public ICollection<Place> PlaceProvinces { get; set; }
        public ICollection<Place> PlaceDistricts { get; set; }
        public ICollection<Place> PlaceCommunes { get; set; }

    }
}
