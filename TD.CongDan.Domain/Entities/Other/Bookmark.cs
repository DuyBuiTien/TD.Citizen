using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Libs.Abstractions.Domain;

namespace TD.CongDan.Domain.Entities.Other
{
    public class Bookmark : AuditableEntity
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public Boolean? IsOwned { get; set; }
        public int? TopicId { get; set; }
        public string TopicTitle { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public string AddressDetail { get; set; }
        public string DateStart { get; set; }
        public string TimeStart { get; set; }
        public string DateEnd { get; set; }
        public string TimeEnd { get; set; }
        public string ContentType { get; set; }

        public string Navigate { get; set; }
        public string Link { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
    }
}
