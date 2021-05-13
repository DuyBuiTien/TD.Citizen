using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Traffic
{
    //Loai phuong tien
    public class VehicleType : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public int SeatCount { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Carpool> Carpools { get; set; }
    }
}
