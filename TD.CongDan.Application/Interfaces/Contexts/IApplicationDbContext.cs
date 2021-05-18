using TD.CongDan.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;
using TD.CongDan.Domain.Entities.Traffic;
using TD.CongDan.Domain.Entities.Other;

namespace TD.CongDan.Application.Interfaces.Contexts
{
    public interface IApplicationDbContext
    {
        IDbConnection Connection { get; }
        bool HasChanges { get; }

        EntityEntry Entry(object entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<Product> Products { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<PlaceType> PlaceTypes { get; set; }
        DbSet<Place> Places { get; set; }

        DbSet<Attachment> Attachments { get; set; }

        DbSet<Area> Areas { get; set; }
        DbSet<AreaType> AreaTypes { get; set; }

        DbSet<Company> Companies { get; set; }
        DbSet<Degree> Degrees { get; set; }
        DbSet<Experience> Experiences { get; set; }
        DbSet<Industry> Industries { get; set; }
        DbSet<CompanyIndustry> CompanyIndustries { get; set; }
        DbSet<JobAge> JobAges { get; set; }
        DbSet<JobApplication> JobApplications { get; set; }
        DbSet<JobName> JobNames { get; set; }
        DbSet<JobPosition> JobPositions { get; set; }
        DbSet<JobType> JobTypes { get; set; }
        DbSet<Recruitment> Recruitments { get; set; }
        DbSet<Salary> Salaries { get; set; }
        DbSet<Benefit> Benefits { get; set; }
        DbSet<RecruitmentBenefit> RecruitmentBenefits { get; set; }
        DbSet<Gender> Genders { get; set; }
        DbSet<IdentityType> IdentityTypes { get; set; }
        DbSet<MaritalStatus> MaritalStatuses { get; set; }
        DbSet<Religion> Religions { get; set; }

        DbSet<Carpool> Carpools { get; set; }
        DbSet<VehicleType> VehicleTypes { get; set; }
        DbSet<Bookmark> Bookmarks { get; set; }

        DbSet<JobApplied> JobApplieds { get; set; }
        DbSet<JobSaved> JobSaveds { get; set; }

    }
}