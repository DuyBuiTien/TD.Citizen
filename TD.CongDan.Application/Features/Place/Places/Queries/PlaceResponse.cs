using System;
using TD.CongDan.Domain.Enums;

namespace TD.CongDan.Application.Features.Places.Queries
{
    public class PlaceResponse
    {
        public int Id { get; set; }
        public string PlaceName { get; set; }
        public string Title { get; set; }
        public string AddressDetail { get; set; }
        public string Source { get; set; }
        public string ExtraInfo { get; set; }
        public string PhoneContact { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string ContentHtml { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public string Tags { get; set; }
        public string Image { get; set; }
        public string Images { get; set; }
        public PlaceStatus Status { get; set; }

        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public int? PlaceTypeId { get; set; }
        public string PlaceType { get; set; }

    }
}