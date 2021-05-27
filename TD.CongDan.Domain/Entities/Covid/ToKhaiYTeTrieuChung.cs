
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Covid
{
    public class ToKhaiYTeTrieuChung : AuditableEntity
    {
        public int TrieuChungId { get; set; }
        public TrieuChung TrieuChung { get; set; }
        public int ToKhaiYTeId { get; set; }
        public ToKhaiYTe ToKhaiYTe { get; set; }
    }
}
