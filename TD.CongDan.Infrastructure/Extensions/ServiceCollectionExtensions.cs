using TD.CongDan.Application.Interfaces.CacheRepositories;
using TD.CongDan.Application.Interfaces.Contexts;
using TD.CongDan.Application.Interfaces.Repositories;
using TD.CongDan.Infrastructure.CacheRepositories;
using TD.CongDan.Infrastructure.DbContexts;
using TD.CongDan.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TD.CongDan.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductCacheRepository, ProductCacheRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IBrandCacheRepository, BrandCacheRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryCacheRepository, CategoryCacheRepository>();
            services.AddTransient<IAttachmentRepository, AttachmentRepository>();
            services.AddTransient<IAreaRepository, AreaRepository>();
            services.AddTransient<IAreaCacheRepository, AreaCacheRepository>();
            services.AddTransient<IAreaTypeRepository, AreaTypeRepository>();
            services.AddTransient<IPlaceTypeRepository, PlaceTypeRepository>();
            services.AddTransient<IPlaceRepository, PlaceRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IDegreeRepository, DegreeRepository>();
            services.AddTransient<IExperienceRepository, ExperienceRepository>();
            services.AddTransient<IIndustryRepository, IndustryRepository>();
            services.AddTransient<IJobAgeRepository, JobAgeRepository>();
            services.AddTransient<IJobApplicationRepository, JobApplicationRepository>();
            services.AddTransient<IJobNameRepository, JobNameRepository>();
            services.AddTransient<IJobPositionRepository, JobPositionRepository>();
            services.AddTransient<IJobTypeRepository, JobTypeRepository>();
            services.AddTransient<IRecruitmentRepository, RecruitmentRepository>();
            services.AddTransient<ISalaryRepository, SalaryRepository>();
            services.AddTransient<IBenefitRepository, BenefitRepository>();

            services.AddTransient<IGenderRepository,GenderRepository>();
            services.AddTransient<IReligionRepository, ReligionRepository>();
            services.AddTransient<IIdentityTypeRepository, IdentityTypeRepository>();
            services.AddTransient<IMaritalStatusRepository, MaritalStatusRepository>();



            #endregion Repositories
        }
    }
}