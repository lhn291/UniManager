using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniManager.Domain.Entities;
using UniManager.Infrastructure.Configuration;

namespace UniManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbConfiguration(configuration);

            services.AddAuth<Student>(configuration, s => s.StudentId, s => s.UserName, s => s.Role);
            services.AddAuth<Lecturer>(configuration, l => l.LecturerId, l => l.UserName, l => l.Role);
            services.AddAuth<Admin>(configuration, a => a.AdminId, a => a.UserName, a => a.Role);

            services.AddAuthorizationPolicies();

            services.AddInfrastructureServices(configuration);

            return services;
        }
    }
}
