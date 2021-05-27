using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Covid
{
    public class TrieuChung : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ICollection<ToKhaiYTeTrieuChung> ToKhaiYTeTrieuChungs { get; set; }
    }
}
