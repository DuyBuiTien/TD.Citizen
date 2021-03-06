using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TD.CongDan.Domain.Entities.Company
{
    //Loai hinh cong viec / ban thoi gian, toan thoi gian
    public class JobType : AuditableEntity
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
