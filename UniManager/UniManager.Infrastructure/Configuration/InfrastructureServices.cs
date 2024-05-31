using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniManager.Application.Interfaces.Persistence;
using UniManager.Application.Interfaces.Services;
using UniManager.Infrastructure.Persistence;
using UniManager.Infrastructure.Services;

namespace UniManager.Infrastructure.Configuration
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseStudentRepository, CourseStudentRepository>();
            services.AddScoped<ILecturerRepository, LecturerRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IIdGeneratorService, IdGeneratorService>();

            services.AddSingleton<IDatetimeProvider, DatetimeProvider>();

            return services;
        }
    }
}
