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
using TD.CongDan.Application.Interfaces;
using TD.CongDan.Infrastructure.Identity.Services;

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

            services.AddTransient<IAttributeDatetimeRepository, AttributeDatetimeRepository>();
            services.AddTransient<IAttributeDecimalRepository, AttributeDecimalRepository>();
            services.AddTransient<IAttributeIntRepository, AttributeIntRepository>();
            services.AddTransient<IAttributeRepository, AttributeRepository>();
            services.AddTransient<IAttributeTextRepository, AttributeTextRepository>();
            services.AddTransient<IAttributeValueRepository, AttributeValueRepository>();
            services.AddTransient<IAttributeVarcharRepository, AttributeVarcharRepository>();
            services.AddTransient<IEcommerceCategoryAttributeRepository, EcommerceCategoryAttributeRepository>();
            services.AddTransient<IEcommerceCategoryProductRepository, EcommerceCategoryProductRepository>();
            services.AddTransient<IEcommerceCategoryRepository, EcommerceCategoryRepository>();

            services.AddTransient<IBrandCacheRepository, BrandCacheRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IAppConfigRepository, AppConfigRepository>();
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
            services.AddTransient<IRecruitmentBenefitRepository, RecruitmentBenefitRepository>();
            services.AddTransient<ICompanyIndustryRepository, CompanyIndustryRepository>();
            services.AddTransient<IJobSavedRepository, JobSavedRepository>();
            services.AddTransient<IJobAppliedRepository, JobAppliedRepository>();
            
            services.AddTransient<IGenderRepository,GenderRepository>();
            services.AddTransient<IReligionRepository, ReligionRepository>();
            services.AddTransient<IIdentityTypeRepository, IdentityTypeRepository>();
            services.AddTransient<IMaritalStatusRepository, MaritalStatusRepository>();

            services.AddTransient<ICarpoolRepository, CarpoolRepository>();
            services.AddTransient<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddTransient<ITrafficTicketRepository, TrafficTicketRepository>();
            services.AddTransient<ILicensePlateRepository, LicensePlateRepository>();

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IBenhNenRepository, BenhNenRepository>();
            services.AddTransient<IChotKiemDichRepository, ChotKiemDichRepository>();
            services.AddTransient<IChucVuRepository, ChucVuRepository>();
            services.AddTransient<IDonViCongTacRepository, DonViCongTacRepository>();
            services.AddTransient<INguoiKhaiBaoRepository, NguoiKhaiBaoRepository>();
            services.AddTransient<IPhuongTienRepository, PhuongTienRepository>();
            services.AddTransient<IQuocGiaRepository, QuocGiaRepository>();
            services.AddTransient<IToKhaiYTeBenhNenRepository, ToKhaiYTeBenhNenRepository>();
            services.AddTransient<IToKhaiYTeRepository, ToKhaiYTeRepository>();
            services.AddTransient<IToKhaiYTeTrieuChungRepository, ToKhaiYTeTrieuChungRepository>();
            services.AddTransient<ITrieuChungRepository, TrieuChungRepository>();

            #endregion Repositories
        }
    }
}