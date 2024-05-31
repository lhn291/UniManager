using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Text;
using UniManager.Application.Interfaces.Authentication;
using UniManager.Application.Interfaces.Services;
using UniManager.Infrastructure.Authentication;

namespace UniManager.Infrastructure.Configuration
{
    public static class AuthConfiguration
    {
        private static bool isBearerSchemeAdded = false;

        public static IServiceCollection AddAuth<TEntity>(
            this IServiceCollection services,
            ConfigurationManager configuration,
            Expression<Func<TEntity, string>> idSelector,
            Expression<Func<TEntity, string>> userNameSelector,
            Expression<Func<TEntity, string>> roleSelector)
            where TEntity : class
        {
            if (!isBearerSchemeAdded)
            {
                var jwtSettings = new JwtSettings();
                configuration.Bind(JwtSettings.SectionName, jwtSettings);
                services.AddSingleton(Options.Create(jwtSettings));

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = jwtSettings.Issuer,
                                ValidAudience = jwtSettings.Audience,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                            };
                        });

                isBearerSchemeAdded = true;
            }

            services.AddSingleton<IJwtTokenGenerator<TEntity>>(sp => new JwtTokenGenerator<TEntity>(
                sp.GetRequiredService<IDatetimeProvider>(),
                sp.GetRequiredService<IOptions<JwtSettings>>(),
                idSelector,
                userNameSelector,
                roleSelector));

            return services;
        }
    }
}
