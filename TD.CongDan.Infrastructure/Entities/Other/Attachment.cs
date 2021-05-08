﻿using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Infrastructure.Entities
{
    public class Attachment : AuditableEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
}
