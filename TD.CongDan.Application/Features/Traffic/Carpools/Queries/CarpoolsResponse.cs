using System;
using System.Collections.Generic;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Traffic;

namespace TD.CongDan.Application.Features.Carpools.Queries
{
    public class CarpoolsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        //Diem khoi hanh
        public int? PlaceDepartureId { get; set; }
        public Place PlaceDeparture { get; set; }
        //Diem den
        public int? PlaceArrivalId { get; set; }
        public Place PlaceArrival { get; set; }


        public DateTime? DepartureDate { get; set; }
        public TimeSpan? DepartureTime { get; set; }
        //Loai phuong tien
        public int? VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
        //Vai tro
        public string Role { get; set; }
        //Gia
        public decimal Price { get; set; }
        //So ghe
        public int SeatCount { get; set; }
        //Trang thai
        public int Status { get; set; }

    }
}