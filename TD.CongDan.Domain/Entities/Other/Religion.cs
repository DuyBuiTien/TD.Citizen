﻿using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.CongDan.Domain.Entities
{
    //Ton giao
    public class Religion : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ICollection<UserInfo> UserInfos { get; set; }
    }
    
}
