using TD.CongDan.Application.Interfaces.Contexts;
using TD.CongDan.Application.Interfaces.Shared;
using TD.CongDan.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TD.Libs.Abstractions.Domain;
using System;
using Microsoft.AspNetCore.Identity;
using TD.CongDan.Infrastructure.Configurations;
using TD.CongDan.Domain.Entities;
using TD.CongDan.Domain.Entities.Company;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TD.CongDan.Infrastructure.Models;
using System.Collections.Generic;
using TD.CongDan.Domain.Enums;
using TD.CongDan.Infrastructure.Extensions;
using TD.CongDan.Domain.Entities.Traffic;
using TD.CongDan.Domain.Entities.Other;

namespace TD.CongDan.Infrastructure.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }

        public DbSet<Audit> AuditLogs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Carpool> Carpools { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<PlaceType> PlaceTypes { get; set; }
        public DbSet<Place> Places { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<Area> Areas { get; set; }
        public DbSet<AreaType> AreaTypes { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<CompanyIndustry> CompanyIndustries { get; set; }
        public DbSet<JobAge> JobAges { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobName> JobNames { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<RecruitmentBenefit> RecruitmentBenefits { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<IdentityType> IdentityTypes { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        public DbSet<JobApplied> JobApplieds { get; set; }
        public DbSet<JobSaved> JobSaveds { get; set; }
        public IDbConnection Connection => Database.GetDbConnection();

        public bool HasChanges => ChangeTracker.HasChanges();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.NowUtc;
                        //entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        entry.Entity.CreatedBy = _authenticatedUser.Username;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTime.NowUtc;
                        //entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        entry.Entity.LastModifiedBy = _authenticatedUser.Username;
                        break;
                }
            }
            //if (_authenticatedUser.UserId == null)
            if (_authenticatedUser.Username == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                //return await SaveChangesAsync(_authenticatedUser.UserId);
                return await SaveChangesAsync(_authenticatedUser.Username);
            }
        }


        public virtual async Task<int> SaveChangesAsync(string userId = null)
        {
            var auditEntries = OnBeforeSaveChanges(userId);
            var result = await base.SaveChangesAsync();
            await OnAfterSaveChanges(auditEntries);
            return result;
        }


        private List<AuditEntry> OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
                AuditLogs.Add(auditEntry.ToAudit());
            }
            return SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AreaTypeConfiguration());
            builder.ApplyConfiguration(new VehicleTypeConfiguration());
            builder.ApplyConfiguration(new AttachmentConfiguration());
            builder.ApplyConfiguration(new PlaceConfiguration());
            builder.ApplyConfiguration(new PlaceTypeConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CompanyConfiguration());
            builder.ApplyConfiguration(new DegreeConfiguration());
            builder.ApplyConfiguration(new ExperienceConfiguration());
            builder.ApplyConfiguration(new IndustryConfiguration());
            builder.ApplyConfiguration(new JobAgeConfiguration());
            builder.ApplyConfiguration(new JobApplicationConfiguration());
            builder.ApplyConfiguration(new JobNameConfiguration());
            builder.ApplyConfiguration(new JobPositionConfiguration());
            builder.ApplyConfiguration(new JobTypeConfiguration());
            builder.ApplyConfiguration(new RecruitmentConfiguration());
            builder.ApplyConfiguration(new SalaryConfiguration());
            builder.ApplyConfiguration(new BenefitConfiguration());
            builder.ApplyConfiguration(new RecruitmentBenefitConfiguration());
            builder.ApplyConfiguration(new CompanyIndustryConfiguration());
            builder.ApplyConfiguration(new JobSavedConfiguration());
            builder.ApplyConfiguration(new JobAppliedConfiguration());

            builder.ApplyConfiguration(new GenderConfiguration());
            builder.ApplyConfiguration(new ReligionConfiguration());
            builder.ApplyConfiguration(new MaritalStatusConfiguration());
            builder.ApplyConfiguration(new IdentityTypeConfiguration());

            builder.ApplyConfiguration(new ApplicationUserConfiguration());

            builder.ApplyConfiguration(new CarpoolConfiguration());
            builder.ApplyConfiguration(new BookmarkConfiguration());
            builder.ApplyConfiguration(new VehicleTypeConfiguration());


            /* builder.Entity<ApplicationUser>(entity =>
             {
                 entity.ToTable(name: "Users");
             });*/
            /* builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
             builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
             builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(x => new { x.UserId, x.RoleId });
             builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(x => x.UserId);

             builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
             builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(x => x.UserId);*/
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Seed();


            /*foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            base.OnModelCreating(builder);*/
        }
    }
}