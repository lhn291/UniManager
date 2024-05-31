using Microsoft.Extensions.DependencyInjection;

namespace UniManager.Infrastructure.Configuration
{
    public static class AuthorizationPolicies
    {
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("LecturerPolicy", policy => policy.RequireRole("Lecturer"));
                options.AddPolicy("StudentPolicy", policy => policy.RequireRole("Student"));
                options.AddPolicy("AdminOrLecturerPolicy", policy => policy.RequireRole("Admin", "Lecturer"));
            });

            return services;
        }
    }
}
