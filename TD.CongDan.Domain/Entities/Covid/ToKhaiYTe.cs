using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Covid
{
    public class ToKhaiYTe : AuditableEntity
    {
        public string NguoiNhap { get; set; }
        public int TrangThai { get; set; }
        public int? NguoiKhaiBaoId { get; set; }
        public NguoiKhaiBao NguoiKhaiBao { get; set; }
        public int? ChotKiemDichId { get; set; }
        public ChotKiemDich ChotKiemDich { get; set; }
        public DateTime? NgayKhoiHanh { get; set; }
        public DateTime? NgayToi { get; set; }
        public int? PhuongTienId {get;set;}
        public PhuongTien PhuongTien { get; set; }

        public string SoPhuongTien { get; set; }
        public string SoGhe { get; set; }

        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? CommuneId { get; set; }
        public string Address { get; set; }

        public string GhiChu { get; set; }

        public Area Province { get; set; }
        public Area District { get; set; }
        public Area Commune { get; set; }


        public ICollection<ToKhaiYTeBenhNen> ToKhaiYTeBenhNens { get; set; }
        public ICollection<ToKhaiYTeTrieuChung> ToKhaiYTeTrieuChungs { get; set; }

    }
}
