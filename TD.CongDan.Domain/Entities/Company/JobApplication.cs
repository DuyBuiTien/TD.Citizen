using TD.Libs.Abstractions.Domain;


namespace TD.CongDan.Domain.Entities.Company
{
    public class JobApplication : AuditableEntity
    {
        // Hồ sơ nghề nghiệp
        public string UserName { get; set; }
        public string Name { get; set; }
        public string CVFile { get; set; }
        //Vi tri hien tai
        public int? CurrentPositionId { get; set; }
        //Vi tri mong muon
        public int? PositionId { get; set; }

        public int? JobNameId { get; set; }

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
        public JobName JobName { get; set; }
        public JobPosition Position { get; set; }
        public Experience Experience { get; set; }
        public Degree Degree { get; set; }
        public JobType JobType { get; set; }

    }
}
