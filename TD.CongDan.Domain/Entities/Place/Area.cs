using System.Collections.Generic;
using System.Text.Json.Serialization;
using TD.CongDan.Domain.Entities.Covid;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities
{
    public class Area : AuditableEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }
        public string ParentCode { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
        public string NameWithType { get; set; }
        public string Path { get; set; }
        public string PathWithType { get; set; }
        public string Description { get; set; }


        [JsonIgnore]
        public ICollection<ApplicationUser> UserInfoProvinces { get; set; }
        [JsonIgnore]
        public ICollection<ApplicationUser> UserInfoDistricts { get; set; }
        [JsonIgnore]
        public ICollection<ApplicationUser> UserInfoCommunes { get; set; }
        [JsonIgnore]
        public ICollection<Place> PlaceProvinces { get; set; }
        [JsonIgnore]
        public ICollection<Place> PlaceDistricts { get; set; }
        [JsonIgnore]
        public ICollection<Place> PlaceCommunes { get; set; }



        [JsonIgnore]
        public ICollection<ChotKiemDich> ChotKiemDichProvinces { get; set; }
        [JsonIgnore]
        public ICollection<ChotKiemDich> ChotKiemDichDistricts { get; set; }
        [JsonIgnore]
        public ICollection<ChotKiemDich> ChotKiemDichCommunes { get; set; }


        [JsonIgnore]
        public ICollection<NguoiKhaiBao> NguoiKhaiBaoProvinces { get; set; }
        [JsonIgnore]
        public ICollection<NguoiKhaiBao> NguoiKhaiBaoDistricts { get; set; }
        [JsonIgnore]
        public ICollection<NguoiKhaiBao> NguoiKhaiBaoCommunes { get; set; }

        [JsonIgnore]
        public ICollection<ToKhaiYTe> ToKhaiYTeProvinces { get; set; }
        [JsonIgnore]
        public ICollection<ToKhaiYTe> ToKhaiYTeDistricts { get; set; }
        [JsonIgnore]
        public ICollection<ToKhaiYTe> ToKhaiYTeCommunes { get; set; }

    }
}
