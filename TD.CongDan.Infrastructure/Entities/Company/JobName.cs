﻿using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.CongDan.Infrastructure.Entities.Company
{
    //Nghe nghiep
    public class JobName : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ICollection<Recruitment> Recruitments { get; set; }
    }
}
