﻿using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Catalog
{
    public class Brand : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
    }
}