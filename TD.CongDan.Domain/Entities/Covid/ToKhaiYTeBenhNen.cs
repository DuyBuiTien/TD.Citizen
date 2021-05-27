
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Covid
{
    public class ToKhaiYTeBenhNen : AuditableEntity
    {
        public int BenhNenId { get; set; }
        public BenhNen BenhNen { get; set; }
        public int ToKhaiYTeId { get; set; }
        public ToKhaiYTe ToKhaiYTe { get; set; }
    }
}
