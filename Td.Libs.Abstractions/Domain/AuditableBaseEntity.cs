using System;
using System.Text.Json.Serialization;

namespace TD.Libs.Abstractions.Domain
{
    public abstract class AuditableEntity : IAuditableBaseEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedOn { get; set; }
        [JsonIgnore]
        public string LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedOn { get; set; }
    }
}
