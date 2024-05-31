using System.Reflection;
using UniManager.API.Common.SwaggerConfigurations;

namespace UniManager.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerConfigure();
            services.AddSwaggerGen();

            return services;
        }
    }
}
