using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.VehicleTypes.Queries
{
    public class VehicleTypesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public int SeatCount { get; set; }
        public string Description { get; set; }


    }
}