using TD.Libs.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.CongDan.Infrastructure.Entities.Company
{
    public class JobApplication : AuditableEntity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string CVFile { get; set; }
        //Vi tri hien tai
        public int? CurrentPositionId { get; set; }
        //Vi tri mong muon
        public int? PositionId { get; set; }
        //Trinh do hoc van
        public int? DegreeId { get; set; }
        //Tong so nam Kinh nghiem
        public int? ExperienceId { get; set; }

        //Mong muon muc luong toi thieu
        public int? MinExpectedSalary { get; set; }
        //Dia diem lam viec
        public string Adrress { get; set; }
        //Hinh thuc lam viec
        public int? JobTypeId { get; set; }
        //Cho phep nguoi khac tim kiem thong tin
        public int IsSearchAllowed { get; set; }

        public JobPosition CurrentPosition { get; set; }
        public JobPosition Position { get; set; }
        public Experience Experience { get; set; }
        public Degree Degree { get; set; }
        public JobType JobType { get; set; }

    }
}
