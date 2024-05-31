using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace UniManager.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
