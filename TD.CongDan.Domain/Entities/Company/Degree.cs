using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TD.CongDan.Domain.Entities.Company
{
    //Bang cap
    public class Degree : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public ICollection<Recruitment> Recruitments { get; set; }
        [JsonIgnore]
        public ICollection<JobApplication> JobApplications { get; set; }
    }
}
