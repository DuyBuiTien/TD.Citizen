using System;
using System.Collections.Generic;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;

namespace TD.CongDan.Application.Features.Recruitments.Queries
{
    public class RecruitmentsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        //Cong ty
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        //Loai hinh cong viec
        public string JobName { get; set; }
        //Vi tri
        public string JobPosition { get; set; }
        //Muc luong
        public string Salary { get; set; }

        public string JobAge { get; set; }


        public DateTime? ResumeApplyExpired { get; set; }
        //So luong
        
        public int Status { get; set; }

        public string PlaceName { get; set; }
        public string PlaceProvince { get; set; }
        public string PlaceDistrict { get; set; }
        public string PlaceCommune { get; set; }

    }
}