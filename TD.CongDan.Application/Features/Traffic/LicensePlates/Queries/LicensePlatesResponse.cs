using System;
using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.LicensePlates.Queries
{
    public class LicensePlatesResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        //Ten chu xe
        public string OwnerFullName { get; set; }
        //Bien so dang ky
        public string LicensePlateNumber { get; set; }
        //Ngay dang ky
        public DateTime? DateOfRegistration { get; set; }

        public string Description { get; set; }


    }
}