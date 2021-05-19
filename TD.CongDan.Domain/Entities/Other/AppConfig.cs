using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Other
{
    public class AppConfig : AuditableEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
