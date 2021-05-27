
using System.Collections.Generic;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Covid
{
    public class QuocGia : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<NguoiKhaiBao> NguoiKhaiBaos { get; set; }
    }
}
